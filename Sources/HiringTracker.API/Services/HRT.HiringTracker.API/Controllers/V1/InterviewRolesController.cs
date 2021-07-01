


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
    public class InterviewRolesController : BaseController
    {
        private readonly Dal.IInterviewRoleDal _dalInterviewRole;
        private readonly ILogger<InterviewRolesController> _logger;


        public InterviewRolesController( Dal.IInterviewRoleDal dalInterviewRole,
                                    ILogger<InterviewRolesController> logger)
        {
            _dalInterviewRole = dalInterviewRole; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalInterviewRole.GetAll();

            IList<DTO.InterviewRole> dtos = new List<DTO.InterviewRole>();

            foreach (var p in entities)
            {
                var dto = InterviewRoleConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{interviewid}/{userid}"), ActionName("GetInterviewRole")]
        public IActionResult Get(System.Int64 interviewid, System.Int64 userid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalInterviewRole.Get(interviewid, userid);
            if (entity != null)
            {
                var dto = InterviewRoleConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"InterviewRole was not found [ids:{interviewid}, {userid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{interviewid}/{userid}"), ActionName("DeleteInterviewRole")]
        public IActionResult Delete(System.Int64 interviewid, System.Int64 userid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalInterviewRole.Get(interviewid, userid);

            if (existingEntity != null)
            {
                bool removed = _dalInterviewRole.Delete(interviewid, userid);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete InterviewRole [ids:{interviewid}, {userid}]");
                }
            }
            else
            {
                response = NotFound($"InterviewRole not found [ids:{interviewid}, {userid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertInterviewRole")]
        public IActionResult Insert(DTO.InterviewRole dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = InterviewRoleConvertor.Convert(dto);

            InterviewRole newEntity = _dalInterviewRole.Insert(entity);

            response = Ok(InterviewRoleConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateInterviewRole")]
        public IActionResult Update(DTO.InterviewRole dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = InterviewRoleConvertor.Convert(dto);

            var existingEntity = _dalInterviewRole.Get(newEntity.InterviewID, newEntity.UserID);
            if (existingEntity != null)
            {
                InterviewRole entity = _dalInterviewRole.Update(newEntity);

                response = Ok(InterviewRoleConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"InterviewRole not found [ids:{newEntity.InterviewID}, {newEntity.UserID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

