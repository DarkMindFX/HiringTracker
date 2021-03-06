


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
    public class CandidateSkillsController : BaseController
    {
        private readonly Dal.ICandidateSkillDal _dalCandidateSkill;
        private readonly Dal.ICandidateDal _dalCandidate;
        private readonly ILogger<CandidateSkillsController> _logger;


        public CandidateSkillsController(   Dal.ICandidateSkillDal dalCandidateSkill,
                                            Dal.ICandidateDal dalCandidate,
                                    ILogger<CandidateSkillsController> logger)
        {
            _dalCandidateSkill = dalCandidateSkill;
            _dalCandidate = dalCandidate;
            _logger = logger;
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
        [HttpGet("bycandidate/{candidateid}"), ActionName("GetCandidateSkillByCandidateID")]
        public IActionResult GetByCandidateID(System.Int64 candidateid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entities = _dalCandidateSkill.GetByCandidateID(candidateid);
            if (entities != null)
            {
                var dto = new List<DTO.CandidateSkill>();
                dto.AddRange(entities.Select(e => { return CandidateSkillConvertor.Convert(e, this.Url); }));
                
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"CandidateSkills were not found [candidateid:{candidateid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("byskill/{skillid}"), ActionName("GetCandidateSkillBySkillID")]
        public IActionResult GetBySkillID(System.Int64 skillid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entities = _dalCandidateSkill.GetBySkillID(skillid);
            if (entities != null)
            {
                var dto = new List<DTO.CandidateSkill>();
                dto.AddRange(entities.Select(e => { return CandidateSkillConvertor.Convert(e, this.Url); }));

                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"CandidateSkills were not found [skillid:{skillid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
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

        [Authorize]
        [HttpPost, ActionName("InsertCandidateSkill")]
        public IActionResult Insert(DTO.CandidateSkill dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = CandidateSkillConvertor.Convert(dto);

            CandidateSkill newEntity = _dalCandidateSkill.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, CandidateSkillConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        [Authorize]
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

        [Authorize]
        [HttpPost("bycandidate/{candidateid}"), ActionName("UpsetCandidateSkills")]
        public IActionResult UpsertCandidateSkills(System.Int64 candidateid, IList<DTO.CandidateSkill> skills)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var candidate = _dalCandidate.Get(candidateid);
            if (candidate != null)
            {

                var entities = new List<CandidateSkill>();
                entities.AddRange(skills.Select(s => CandidateSkillConvertor.Convert(s)));

                _dalCandidateSkill.SetCandidateSkills(candidateid, entities);

                response = Ok();
            }
            else
            {
                response = NotFound($"Candidate was not found: [candidateid:{candidateid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

