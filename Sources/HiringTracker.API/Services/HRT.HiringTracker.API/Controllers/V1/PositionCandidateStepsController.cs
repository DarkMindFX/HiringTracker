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
    [Route("api/v1/[controller]")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class PositionCandidateStepsController : ControllerBase
    {

        private readonly ILogger<PositionCandidateStepsController> _logger;
        private readonly IPositionCandidateStepDal _dalPosCandidateSteps;

        public PositionCandidateStepsController(ILogger<PositionCandidateStepsController> logger,
                                IPositionCandidateStepDal dalPosCandidateSteps)
        {
            _logger = logger;
            _dalPosCandidateSteps = dalPosCandidateSteps;

        }

        [HttpGet]
        public IActionResult GetPositionCandidateSteps()
        {
            IActionResult response = null;

            var statuses = _dalPosCandidateSteps.GetAll();
            if (statuses != null)
            {
                List<HRT.DTO.PositionCandidateStep> content = new List<HRT.DTO.PositionCandidateStep>();
                foreach (var u in statuses)
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
