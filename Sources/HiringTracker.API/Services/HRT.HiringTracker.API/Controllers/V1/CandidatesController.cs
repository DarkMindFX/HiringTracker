using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HRT.HiringTracker.API.Filters;
using HRT.HiringTracker.API.Helpers;
using HRT.Interfaces;
using HRT.Interfaces.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRT.HiringTracker.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class CandidatesController : ControllerBase
    {
        private readonly Dal.ICandidateDal _dalCandidate;
        private readonly Dal.IUserDal _dalUser;
        private readonly Dal.ISkillDal _dalSkill;
        private readonly Dal.ISkillProficiencyDal _dalSkillProfs;
        private readonly ILogger<CandidatesController> _logger;


        public CandidatesController(Dal.ICandidateDal dalCandidate,
                                    Dal.IUserDal dalUser,
                                    Dal.ISkillDal dalSkill,
                                    Dal.ISkillProficiencyDal dalSkillProfs,
                                    ILogger<CandidatesController> logger)
        {
            _dalCandidate = dalCandidate;
            _dalUser = dalUser;
            _dalSkill = dalSkill;
            _dalSkillProfs = dalSkillProfs;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetCandidates()
        {
            IActionResult response = null;

            var candidates = _dalCandidate.GetAll();
            var users = _dalUser.GetAllAsDictionary();

            IList<DTO.Candidate> dtos = new List<DTO.Candidate>();

            foreach (var p in candidates)
            {
                var dto = EntityToDtoConvertor.Convert(p, users, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            return response;
        }

        [Authorize]
        [HttpGet("{id}"), ActionName("GetCandidate")]
        public IActionResult GetCandidate(long id)
        {
            IActionResult response = null;
            var users = _dalUser.GetAllAsDictionary();

            var entity = _dalCandidate.Get(id);
            if (entity != null)
            {
                var dto = EntityToDtoConvertor.Convert(entity, users, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"Candidate was not found [id:{id}]");
            }

            return response;
        }

        [Authorize]
        [HttpDelete("{id}"), ActionName("DeleteCandidate")]
        public IActionResult DeleteCandidate(long id)
        {
            IActionResult response = null;

            bool removed = _dalCandidate.Delete(id);
            if (removed)
            {
                response = Ok();
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete position [id:{id}]");
            }

            return response;
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddCandidate(DTO.CandidateUpsert dto)
        {
            IActionResult response = UpsertCandidate(dto);

            return response;
        }


        [Authorize]
        [HttpPut, ActionName("UpdateCandidate")]
        public IActionResult UpsertCandidate(DTO.CandidateUpsert dto)
        {
            IActionResult response = null;

            var entity = EntityToDtoConvertor.Convert(dto.Candidate);

            User editor = HttpContext.Items["User"] as User;

            long? id = _dalCandidate.Upsert(entity, editor != null ? editor.UserID : null);

            if (dto.Candidate.CandidateID != null || id != null)
            {
                var posSkills = new List<CandidateSkill>();
                foreach (var s in dto.Skills)
                {
                    posSkills.Add(EntityToDtoConvertor.Convert(s));
                }

                _dalCandidate.SetSkills(dto.Candidate.CandidateID ?? (long)id, posSkills);
            }

            response = Ok(new DTO.CandidateUpsertResponse() { CandidateID = dto.Candidate.CandidateID ?? (long)id });

            return response;
        }

        [Authorize]
        [HttpGet("{id}/skills"), ActionName("GetCandidateSkills")]
        public IActionResult GetCandidateSkills(long id)
        {
            IActionResult response = null;

            var entity = _dalCandidate.Get(id);
            if (entity != null)
            {
                var entities = _dalCandidate.GetSkills(id);
                var dtos = new List<DTO.CandidateSkill>();

                var skills = _dalSkill.GetAllAsDictionary();
                var profs = _dalSkillProfs.GetAllAsDictionary();

                foreach (var e in entities)
                {
                    var dto = EntityToDtoConvertor.Convert(e, skills, profs, this.Url);
                    dtos.Add(dto);
                }

                response = Ok(dtos);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"Candidate was not found [id:{id}]");
            }

            return response;
        }
    }

}
