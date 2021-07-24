


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HRT.HiringTracker.API.Filters;
using HRT.Interfaces.Entities;
using HRT.Utils.Convertors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace HRT.HiringTracker.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class UserRolePositionsController : BaseController
    {
        private readonly Dal.IUserRolePositionDal _dalUserRolePosition;
        private readonly ILogger<UserRolePositionsController> _logger;


        public UserRolePositionsController( Dal.IUserRolePositionDal dalUserRolePosition,
                                    ILogger<UserRolePositionsController> logger)
        {
            _dalUserRolePosition = dalUserRolePosition; 
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalUserRolePosition.GetAll();

            IList<DTO.UserRolePosition> dtos = new List<DTO.UserRolePosition>();

            foreach (var p in entities)
            {
                var dto = UserRolePositionConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("{positionid}/{userid}"), ActionName("GetUserRolePosition")]
        public IActionResult Get(System.Int64 positionid, System.Int64 userid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalUserRolePosition.Get(positionid, userid);
            if (entity != null)
            {
                var dto = UserRolePositionConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"UserRolePosition was not found [ids:{positionid}, {userid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpDelete("{positionid}/{userid}"), ActionName("DeleteUserRolePosition")]
        public IActionResult Delete(System.Int64 positionid, System.Int64 userid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalUserRolePosition.Get(positionid, userid);

            if (existingEntity != null)
            {
                bool removed = _dalUserRolePosition.Delete(positionid, userid);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete UserRolePosition [ids:{positionid}, {userid}]");
                }
            }
            else
            {
                response = NotFound($"UserRolePosition not found [ids:{positionid}, {userid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpPost, ActionName("InsertUserRolePosition")]
        public IActionResult Insert(DTO.UserRolePosition dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = UserRolePositionConvertor.Convert(dto);

            UserRolePosition newEntity = _dalUserRolePosition.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, UserRolePositionConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        [Authorize]
        [HttpPut, ActionName("UpdateUserRolePosition")]
        public IActionResult Update(DTO.UserRolePosition dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = UserRolePositionConvertor.Convert(dto);

            var existingEntity = _dalUserRolePosition.Get(newEntity.PositionID, newEntity.UserID);
            if (existingEntity != null)
            {
                UserRolePosition entity = _dalUserRolePosition.Update(newEntity);

                response = Ok(UserRolePositionConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"UserRolePosition not found [ids:{newEntity.PositionID}, {newEntity.UserID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

