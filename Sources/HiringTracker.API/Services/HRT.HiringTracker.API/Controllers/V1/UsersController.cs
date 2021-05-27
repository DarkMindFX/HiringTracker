using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HRT.HiringTracker.API.Filters;
using HRT.HiringTracker.API.Helpers;
using HRT.Interfaces;
using HRT.Interfaces.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HRT.HiringTracker.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class UsersController : ControllerBase
    {
        private readonly IUserDal _dalUser;
        private readonly ILogger<UsersController> _logger;
        private readonly IOptions<AppSettings> _appSettings;

        public UsersController( IOptions<AppSettings> appSettings,
                                IUserDal dalUser,
                                ILogger<UsersController> logger)
        {
            _dalUser = dalUser;            
            _logger = logger;
            _appSettings = appSettings;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(DTO.LoginRequest request)
        {
            IActionResult response = null;

            var entity = _dalUser.GetByLogin(request.Login);
            if(entity != null)
            {
                string pwdHash = PasswordHelper.GenerateHash(request.Password, entity.Salt);
                if(pwdHash.Equals(entity.PasswordHash))
                {
                    var dto = new DTO.LoginResponse()
                    {
                        User = EntityToDtoConvertor.Convert(entity, this.Url),
                        Token = GenerateToken(entity),
                        Expires = DateTime.Now + TimeSpan.FromSeconds(_appSettings.Value.SessionTimeout)
                    };

                    response = Ok(dto);
                }
                else
                {
                    response = StatusCode( (int)HttpStatusCode.Forbidden, "User with given login/password not found");
                }
            }
            else
            {
                response = NotFound($"User with given credentials not found [login: {request.Login}]");
            }

            return response;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUsers()
        {
            IActionResult response = null;

            var entities = _dalUser.GetAll();

            var dtos = new List<DTO.User>();

            foreach(var e in entities)
            {
                dtos.Add(EntityToDtoConvertor.Convert(e, this.Url));
            }

            response = Ok(dtos);

            return response; 
        }

        [Authorize]
        [HttpGet("{id}"), ActionName("GetUser")]
        public IActionResult GetUser(long id)
        {
            IActionResult response = null;

            var entity = _dalUser.Get(id);

            if(entity != null)
            {
                var dto = EntityToDtoConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = NotFound($"User with given ID was not found [id:{id}]");
            }

            return response;
        }

        [Authorize]
        [HttpDelete("{id}"), ActionName("DeleteUser")]
        public IActionResult DeleteUser(long id)
        {
            IActionResult response = null;

            var entity = _dalUser.Get(id);

            if (entity != null)
            {
                bool removed = _dalUser.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    throw new InvalidOperationException($"Failed to remove user [id:{id}]");
                }
            }
            else
            {
                response = NotFound($"User with given ID was not found [id:{id}]");
            }

            return response;
        }

        [Authorize]
        [HttpPut, ActionName("UpdateUser")]
        [HttpPost]
        public IActionResult UpsertUser(DTO.User dto)
        {
            IActionResult response = null;

            var entity = EntityToDtoConvertor.Convert(dto);
            if(entity.UserID == null || !string.IsNullOrEmpty(dto.Password))
            {
                // new user generating salt
                if (entity.UserID == null)
                {
                    entity.Salt = PasswordHelper.GenerateSalt(10);
                }

                if (!string.IsNullOrEmpty(dto.Password))
                {
                    entity.PasswordHash = PasswordHelper.GenerateHash(dto.Password, entity.Salt);
                }
            }

            User editor = HttpContext.Items["User"] as User;

            long? id = _dalUser.Upsert(entity, editor.UserID);

            response = GetUser(dto.UserID ?? (long)id);

            return response;
        }

        #region Support method

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                    {
                            new Claim("id", user.UserID.ToString())
                    }
                ),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            string sToken = tokenHandler.WriteToken(token);

            return sToken;
        }

        #endregion

    }
}
