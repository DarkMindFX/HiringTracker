


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
    public class UserRoleCandidatesController : BaseController
    {
        private readonly Dal.IUserRoleCandidateDal _dalUserRoleCandidate;
        private readonly ILogger<UserRoleCandidatesController> _logger;


        public UserRoleCandidatesController( Dal.IUserRoleCandidateDal dalUserRoleCandidate,
                                    ILogger<UserRoleCandidatesController> logger)
        {
            _dalUserRoleCandidate = dalUserRoleCandidate; 
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalUserRoleCandidate.GetAll();

            IList<DTO.UserRoleCandidate> dtos = new List<DTO.UserRoleCandidate>();

            foreach (var p in entities)
            {
                var dto = UserRoleCandidateConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("{candidateid}/{userid}"), ActionName("GetUserRoleCandidate")]
        public IActionResult Get(System.Int64 candidateid, System.Int64 userid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalUserRoleCandidate.Get(candidateid, userid);
            if (entity != null)
            {
                var dto = UserRoleCandidateConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"UserRoleCandidate was not found [ids:{candidateid}, {userid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpDelete("{candidateid}/{userid}"), ActionName("DeleteUserRoleCandidate")]
        public IActionResult Delete(System.Int64 candidateid, System.Int64 userid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalUserRoleCandidate.Get(candidateid, userid);

            if (existingEntity != null)
            {
                bool removed = _dalUserRoleCandidate.Delete(candidateid, userid);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete UserRoleCandidate [ids:{candidateid}, {userid}]");
                }
            }
            else
            {
                response = NotFound($"UserRoleCandidate not found [ids:{candidateid}, {userid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpPost, ActionName("InsertUserRoleCandidate")]
        public IActionResult Insert(DTO.UserRoleCandidate dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = UserRoleCandidateConvertor.Convert(dto);

            UserRoleCandidate newEntity = _dalUserRoleCandidate.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, UserRoleCandidateConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        [Authorize]
        [HttpPut, ActionName("UpdateUserRoleCandidate")]
        public IActionResult Update(DTO.UserRoleCandidate dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = UserRoleCandidateConvertor.Convert(dto);

            var existingEntity = _dalUserRoleCandidate.Get(newEntity.CandidateID, newEntity.UserID);
            if (existingEntity != null)
            {
                UserRoleCandidate entity = _dalUserRoleCandidate.Update(newEntity);

                response = Ok(UserRoleCandidateConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"UserRoleCandidate not found [ids:{newEntity.CandidateID}, {newEntity.UserID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

