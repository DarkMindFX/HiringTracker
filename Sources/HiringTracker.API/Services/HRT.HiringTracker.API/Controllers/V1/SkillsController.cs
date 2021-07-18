


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
    public class SkillsController : BaseController
    {
        private readonly Dal.ISkillDal _dalSkill;
        private readonly ILogger<SkillsController> _logger;


        public SkillsController( Dal.ISkillDal dalSkill,
                                    ILogger<SkillsController> logger)
        {
            _dalSkill = dalSkill; 
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalSkill.GetAll();

            IList<DTO.Skill> dtos = new List<DTO.Skill>();

            foreach (var p in entities)
            {
                var dto = SkillConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("{id}"), ActionName("GetSkill")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalSkill.Get(id);
            if (entity != null)
            {
                var dto = SkillConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"Skill was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpDelete("{id}"), ActionName("DeleteSkill")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalSkill.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalSkill.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete Skill [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"Skill not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpPost, ActionName("InsertSkill")]
        public IActionResult Insert(DTO.Skill dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = SkillConvertor.Convert(dto);

            Skill newEntity = _dalSkill.Insert(entity);

            response = Ok(SkillConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        [Authorize]
        [HttpPut, ActionName("UpdateSkill")]
        public IActionResult Update(DTO.Skill dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = SkillConvertor.Convert(dto);

            var existingEntity = _dalSkill.Get(newEntity.ID);
            if (existingEntity != null)
            {
                Skill entity = _dalSkill.Update(newEntity);

                response = Ok(SkillConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"Skill not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

