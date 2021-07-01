


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
    public class InterviewFeedbacksController : BaseController
    {
        private readonly Dal.IInterviewFeedbackDal _dalInterviewFeedback;
        private readonly ILogger<InterviewFeedbacksController> _logger;


        public InterviewFeedbacksController( Dal.IInterviewFeedbackDal dalInterviewFeedback,
                                    ILogger<InterviewFeedbacksController> logger)
        {
            _dalInterviewFeedback = dalInterviewFeedback; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalInterviewFeedback.GetAll();

            IList<DTO.InterviewFeedback> dtos = new List<DTO.InterviewFeedback>();

            foreach (var p in entities)
            {
                var dto = InterviewFeedbackConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetInterviewFeedback")]
        public IActionResult Get(System.Int64 id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalInterviewFeedback.Get(id);
            if (entity != null)
            {
                var dto = InterviewFeedbackConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"InterviewFeedback was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteInterviewFeedback")]
        public IActionResult Delete(System.Int64 id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalInterviewFeedback.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalInterviewFeedback.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete InterviewFeedback [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"InterviewFeedback not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertInterviewFeedback")]
        public IActionResult Insert(DTO.InterviewFeedback dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = InterviewFeedbackConvertor.Convert(dto);

            InterviewFeedback newEntity = _dalInterviewFeedback.Insert(entity);

            response = Ok(InterviewFeedbackConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateInterviewFeedback")]
        public IActionResult Update(DTO.InterviewFeedback dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = InterviewFeedbackConvertor.Convert(dto);

            var existingEntity = _dalInterviewFeedback.Get(newEntity.ID);
            if (existingEntity != null)
            {
                InterviewFeedback entity = _dalInterviewFeedback.Update(newEntity);

                response = Ok(InterviewFeedbackConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"InterviewFeedback not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

