


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
    public class CandidateSkillsController : BaseController
    {
        private readonly Dal.ICandidateSkillDal _dalCandidateSkill;
        private readonly ILogger<CandidateSkillsController> _logger;


        public CandidateSkillsController( Dal.ICandidateSkillDal dalCandidateSkill,
                                    ILogger<CandidateSkillsController> logger)
        {
            _dalCandidateSkill = dalCandidateSkill; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalCandidateSkill.GetAll();

            IList<DTO.CandidateSkill> dtos = new List<DTO.CandidateSkill>();

            foreach (var p in entities)
            {
                var dto = CandidateSkillConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{candidateid}/{skillid}"), ActionName("GetCandidateSkill")]
        public IActionResult Get(System.Int64 candidateid, System.Int64 skillid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalCandidateSkill.Get(candidateid, skillid);
            if (entity != null)
            {
                var dto = CandidateSkillConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"CandidateSkill was not found [ids:{candidateid}, {skillid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{candidateid}/{skillid}"), ActionName("DeleteCandidateSkill")]
        public IActionResult Delete(System.Int64 candidateid, System.Int64 skillid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalCandidateSkill.Get(candidateid, skillid);

            if (existingEntity != null)
            {
                bool removed = _dalCandidateSkill.Delete(candidateid, skillid);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete CandidateSkill [ids:{candidateid}, {skillid}]");
                }
            }
            else
            {
                response = NotFound($"CandidateSkill not found [ids:{candidateid}, {skillid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertCandidateSkill")]
        public IActionResult Insert(DTO.CandidateSkill dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = CandidateSkillConvertor.Convert(dto);

            CandidateSkill newEntity = _dalCandidateSkill.Insert(entity);

            response = Ok(CandidateSkillConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateCandidateSkill")]
        public IActionResult Update(DTO.CandidateSkill dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = CandidateSkillConvertor.Convert(dto);

            var existingEntity = _dalCandidateSkill.Get(newEntity.CandidateID, newEntity.SkillID);
            if (existingEntity != null)
            {
                CandidateSkill entity = _dalCandidateSkill.Update(newEntity);

                response = Ok(CandidateSkillConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"CandidateSkill not found [ids:{newEntity.CandidateID}, {newEntity.SkillID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

