


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
    public class InterviewStatusesController : BaseController
    {
        private readonly Dal.IInterviewStatusDal _dalInterviewStatus;
        private readonly ILogger<InterviewStatusesController> _logger;


        public InterviewStatusesController( Dal.IInterviewStatusDal dalInterviewStatus,
                                    ILogger<InterviewStatusesController> logger)
        {
            _dalInterviewStatus = dalInterviewStatus; 
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalInterviewStatus.GetAll();

            IList<DTO.InterviewStatus> dtos = new List<DTO.InterviewStatus>();

            foreach (var p in entities)
            {
                var dto = InterviewStatusConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("{id}"), ActionName("GetInterviewStatus")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalInterviewStatus.Get(id);
            if (entity != null)
            {
                var dto = InterviewStatusConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"InterviewStatus was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpDelete("{id}"), ActionName("DeleteInterviewStatus")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalInterviewStatus.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalInterviewStatus.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete InterviewStatus [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"InterviewStatus not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpPost, ActionName("InsertInterviewStatus")]
        public IActionResult Insert(DTO.InterviewStatus dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = InterviewStatusConvertor.Convert(dto);

            InterviewStatus newEntity = _dalInterviewStatus.Insert(entity);

            response = Ok(InterviewStatusConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        [Authorize]
        [HttpPut, ActionName("UpdateInterviewStatus")]
        public IActionResult Update(DTO.InterviewStatus dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = InterviewStatusConvertor.Convert(dto);

            var existingEntity = _dalInterviewStatus.Get(newEntity.ID);
            if (existingEntity != null)
            {
                InterviewStatus entity = _dalInterviewStatus.Update(newEntity);

                response = Ok(InterviewStatusConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"InterviewStatus not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

