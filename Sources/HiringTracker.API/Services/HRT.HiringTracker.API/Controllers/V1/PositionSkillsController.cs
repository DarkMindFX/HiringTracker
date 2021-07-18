


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
    public class PositionSkillsController : BaseController
    {
        private readonly Dal.IPositionSkillDal _dalPositionSkill;
        private readonly ILogger<PositionSkillsController> _logger;


        public PositionSkillsController( Dal.IPositionSkillDal dalPositionSkill,
                                    ILogger<PositionSkillsController> logger)
        {
            _dalPositionSkill = dalPositionSkill; 
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalPositionSkill.GetAll();

            IList<DTO.PositionSkill> dtos = new List<DTO.PositionSkill>();

            foreach (var p in entities)
            {
                var dto = PositionSkillConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("{positionid}/{skillid}"), ActionName("GetPositionSkill")]
        public IActionResult Get(System.Int64 positionid, System.Int64 skillid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalPositionSkill.Get(positionid, skillid);
            if (entity != null)
            {
                var dto = PositionSkillConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"PositionSkill was not found [ids:{positionid}, {skillid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpDelete("{positionid}/{skillid}"), ActionName("DeletePositionSkill")]
        public IActionResult Delete(System.Int64 positionid, System.Int64 skillid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalPositionSkill.Get(positionid, skillid);

            if (existingEntity != null)
            {
                bool removed = _dalPositionSkill.Delete(positionid, skillid);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete PositionSkill [ids:{positionid}, {skillid}]");
                }
            }
            else
            {
                response = NotFound($"PositionSkill not found [ids:{positionid}, {skillid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpPost, ActionName("InsertPositionSkill")]
        public IActionResult Insert(DTO.PositionSkill dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = PositionSkillConvertor.Convert(dto);

            PositionSkill newEntity = _dalPositionSkill.Insert(entity);

            response = Ok(PositionSkillConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        [Authorize]
        [HttpPut, ActionName("UpdatePositionSkill")]
        public IActionResult Update(DTO.PositionSkill dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = PositionSkillConvertor.Convert(dto);

            var existingEntity = _dalPositionSkill.Get(newEntity.PositionID, newEntity.SkillID);
            if (existingEntity != null)
            {
                PositionSkill entity = _dalPositionSkill.Update(newEntity);

                response = Ok(PositionSkillConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"PositionSkill not found [ids:{newEntity.PositionID}, {newEntity.SkillID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

