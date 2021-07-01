


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
    public class CandidatePropertiesController : BaseController
    {
        private readonly Dal.ICandidatePropertyDal _dalCandidateProperty;
        private readonly ILogger<CandidatePropertiesController> _logger;


        public CandidatePropertiesController( Dal.ICandidatePropertyDal dalCandidateProperty,
                                    ILogger<CandidatePropertiesController> logger)
        {
            _dalCandidateProperty = dalCandidateProperty; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalCandidateProperty.GetAll();

            IList<DTO.CandidateProperty> dtos = new List<DTO.CandidateProperty>();

            foreach (var p in entities)
            {
                var dto = CandidatePropertyConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetCandidateProperty")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalCandidateProperty.Get(id);
            if (entity != null)
            {
                var dto = CandidatePropertyConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"CandidateProperty was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteCandidateProperty")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalCandidateProperty.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalCandidateProperty.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete CandidateProperty [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"CandidateProperty not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertCandidateProperty")]
        public IActionResult Insert(DTO.CandidateProperty dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = CandidatePropertyConvertor.Convert(dto);

            CandidateProperty newEntity = _dalCandidateProperty.Insert(entity);

            response = Ok(CandidatePropertyConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateCandidateProperty")]
        public IActionResult Update(DTO.CandidateProperty dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = CandidatePropertyConvertor.Convert(dto);

            var existingEntity = _dalCandidateProperty.Get(newEntity.ID);
            if (existingEntity != null)
            {
                CandidateProperty entity = _dalCandidateProperty.Update(newEntity);

                response = Ok(CandidatePropertyConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"CandidateProperty not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

