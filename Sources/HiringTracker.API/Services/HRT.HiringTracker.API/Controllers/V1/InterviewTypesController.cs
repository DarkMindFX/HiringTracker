


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HRT.HiringTracker.API.Filters;
using HRT.Interfaces.Entities;
using HRT.Utils.Convertors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;


namespace HRT.HiringTracker.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class InterviewTypesController : BaseController
    {
        private readonly Dal.IInterviewTypeDal _dalInterviewType;
        private readonly ILogger<InterviewTypesController> _logger;


        public InterviewTypesController( Dal.IInterviewTypeDal dalInterviewType,
                                    ILogger<InterviewTypesController> logger)
        {
            _dalInterviewType = dalInterviewType; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalInterviewType.GetAll();

            IList<DTO.InterviewType> dtos = new List<DTO.InterviewType>();

            foreach (var p in entities)
            {
                var dto = InterviewTypeConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetInterviewType")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalInterviewType.Get(id);
            if (entity != null)
            {
                var dto = InterviewTypeConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"InterviewType was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteInterviewType")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalInterviewType.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalInterviewType.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete InterviewType [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"InterviewType not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertInterviewType")]
        public IActionResult Insert(DTO.InterviewType dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = InterviewTypeConvertor.Convert(dto);

            InterviewType newEntity = _dalInterviewType.Insert(entity);

            response =StatusCode((int)HttpStatusCode.Created, InterviewTypeConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateInterviewType")]
        public IActionResult Update(DTO.InterviewType dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = InterviewTypeConvertor.Convert(dto);

            var existingEntity = _dalInterviewType.Get(newEntity.ID);
            if (existingEntity != null)
            {
                InterviewType entity = _dalInterviewType.Update(newEntity);

                response = Ok(InterviewTypeConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"InterviewType not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

