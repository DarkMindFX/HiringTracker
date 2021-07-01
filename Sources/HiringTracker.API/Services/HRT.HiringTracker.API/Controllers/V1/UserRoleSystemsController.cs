


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HRT.HiringTracker.API.Filters;
using HRT.Interfaces.Entities;
using HRT.Utils.Convertors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace HRT.HiringTracker.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class UserRoleSystemsController : BaseController
    {
        private readonly Dal.IUserRoleSystemDal _dalUserRoleSystem;
        private readonly ILogger<UserRoleSystemsController> _logger;


        public UserRoleSystemsController( Dal.IUserRoleSystemDal dalUserRoleSystem,
                                    ILogger<UserRoleSystemsController> logger)
        {
            _dalUserRoleSystem = dalUserRoleSystem; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalUserRoleSystem.GetAll();

            IList<DTO.UserRoleSystem> dtos = new List<DTO.UserRoleSystem>();

            foreach (var p in entities)
            {
                var dto = UserRoleSystemConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{userid}/{roleid}"), ActionName("GetUserRoleSystem")]
        public IActionResult Get(System.Int64 userid, System.Int64 roleid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalUserRoleSystem.Get(userid, roleid);
            if (entity != null)
            {
                var dto = UserRoleSystemConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"UserRoleSystem was not found [ids:{userid}, {roleid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{userid}/{roleid}"), ActionName("DeleteUserRoleSystem")]
        public IActionResult Delete(System.Int64 userid, System.Int64 roleid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalUserRoleSystem.Get(userid, roleid);

            if (existingEntity != null)
            {
                bool removed = _dalUserRoleSystem.Delete(userid, roleid);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete UserRoleSystem [ids:{userid}, {roleid}]");
                }
            }
            else
            {
                response = NotFound($"UserRoleSystem not found [ids:{userid}, {roleid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertUserRoleSystem")]
        public IActionResult Insert(DTO.UserRoleSystem dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = UserRoleSystemConvertor.Convert(dto);

            UserRoleSystem newEntity = _dalUserRoleSystem.Insert(entity);

            response = Ok(UserRoleSystemConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateUserRoleSystem")]
        public IActionResult Update(DTO.UserRoleSystem dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = UserRoleSystemConvertor.Convert(dto);

            var existingEntity = _dalUserRoleSystem.Get(newEntity.UserID, newEntity.RoleID);
            if (existingEntity != null)
            {
                UserRoleSystem entity = _dalUserRoleSystem.Update(newEntity);

                response = Ok(UserRoleSystemConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"UserRoleSystem not found [ids:{newEntity.UserID}, {newEntity.RoleID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

