using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HRT.HiringTracker.API.Filters;
using HRT.HiringTracker.API.Helpers;
using HRT.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRT.HiringTracker.API.Controllers.V1
{

    [ApiController]
    [UnhandledExceptionFilter]
    [Route("api/v1/[controller]")]

    public class UtilsController : ControllerBase
    {
        private readonly ILogger<UtilsController> _logger;
        private readonly IPositionStatusDal _dalPosStatus;
        private readonly IPositionCandidateStatusDal _dalPosCandidateStatus;
        private readonly IPositionCandidateStepDal _dalPosCandidateStep;
        private readonly ISkillDal _dalSkill;
        private readonly ISkillProficiencyDal _dalSillProficiency;
        public UtilsController(ILogger<UtilsController> logger,
                                IPositionStatusDal dalPosStatus,
                                IPositionCandidateStatusDal dalPosCandidateStatus,
                                IPositionCandidateStepDal dalPosCandidateStep,
                                ISkillDal dalSkill,
                                ISkillProficiencyDal dalSillProficiency)
        {
            _logger = logger;
            _dalPosStatus = dalPosStatus;
            _dalPosCandidateStatus = dalPosCandidateStatus;
            _dalPosCandidateStep = dalPosCandidateStep;
            _dalSkill = dalSkill;
            _dalSillProficiency = dalSillProficiency;
        }

        [HttpGet("/position_statuses")]
        public IActionResult GetPositionStatuses()
        {
            IActionResult response = null;

            var statuses = _dalPosStatus.GetAll();
            if(statuses != null)
            {
                List<HRT.DTO.PositionStatus> content = new List<HRT.DTO.PositionStatus>();
                foreach (var u in statuses)
                {
                    content.Add(EntityToDtoConvertor.Convert(u, this.Url));
                }

                return Ok(content);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.InternalServerError, "Failed to obtain list of Position Statuses");
            }

            return response;
        }

        [HttpGet("/position_candidate_statuses")]
        public IActionResult GetPositionCandidateStatuses()
        {
            IActionResult response = null;

            var statuses = _dalPosCandidateStatus.GetAll();
            if (statuses != null)
            {
                List<HRT.DTO.PositionCandidateStatus> content = new List<HRT.DTO.PositionCandidateStatus>();
                foreach (var u in statuses)
                {
                    content.Add(EntityToDtoConvertor.Convert(u, this.Url));
                }

                return Ok(content);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.InternalServerError, "Failed to obtain list of Position Candidate Statuses");
            }

            return response;
        }

        [HttpGet("/position_candidate_steps")]
        public IActionResult GetPositionCandidateSteps()
        {
            IActionResult response = null;

            var steps = _dalPosCandidateStep.GetAll();
            if (steps != null)
            {
                List<HRT.DTO.PositionCandidateStep> content = new List<HRT.DTO.PositionCandidateStep>();
                foreach (var u in steps)
                {
                    content.Add(EntityToDtoConvertor.Convert(u, this.Url));
                }

                return Ok(content);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.InternalServerError, "Failed to obtain list of Position Candidate Steps");
            }

            return response;
        }


    }
}
