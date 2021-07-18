


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
    public class SkillProficienciesController : BaseController
    {
        private readonly Dal.ISkillProficiencyDal _dalSkillProficiency;
        private readonly ILogger<SkillProficienciesController> _logger;


        public SkillProficienciesController( Dal.ISkillProficiencyDal dalSkillProficiency,
                                    ILogger<SkillProficienciesController> logger)
        {
            _dalSkillProficiency = dalSkillProficiency; 
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalSkillProficiency.GetAll();

            IList<DTO.SkillProficiency> dtos = new List<DTO.SkillProficiency>();

            foreach (var p in entities)
            {
                var dto = SkillProficiencyConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("{id}"), ActionName("GetSkillProficiency")]
        public IActionResult Get(System.Int64 id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalSkillProficiency.Get(id);
            if (entity != null)
            {
                var dto = SkillProficiencyConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"SkillProficiency was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpDelete("{id}"), ActionName("DeleteSkillProficiency")]
        public IActionResult Delete(System.Int64 id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalSkillProficiency.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalSkillProficiency.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete SkillProficiency [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"SkillProficiency not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpPost, ActionName("InsertSkillProficiency")]
        public IActionResult Insert(DTO.SkillProficiency dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = SkillProficiencyConvertor.Convert(dto);

            SkillProficiency newEntity = _dalSkillProficiency.Insert(entity);

            response = Ok(SkillProficiencyConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        [Authorize]
        [HttpPut, ActionName("UpdateSkillProficiency")]
        public IActionResult Update(DTO.SkillProficiency dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = SkillProficiencyConvertor.Convert(dto);

            var existingEntity = _dalSkillProficiency.Get(newEntity.ID);
            if (existingEntity != null)
            {
                SkillProficiency entity = _dalSkillProficiency.Update(newEntity);

                response = Ok(SkillProficiencyConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"SkillProficiency not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

