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
    public class ProposalStepsController : ControllerBase
    {

        private readonly ILogger<ProposalStepsController> _logger;
        private readonly Dal.IProposalStepDal _dalPosCandidateSteps;

        public ProposalStepsController(ILogger<ProposalStepsController> logger,
                                Dal.IProposalStepDal dalPosCandidateSteps)
        {
            _logger = logger;
            _dalPosCandidateSteps = dalPosCandidateSteps;

        }

        [HttpGet]
        public IActionResult GetProposalSteps()
        {
            IActionResult response = null;

            var statuses = _dalPosCandidateSteps.GetAll();
            if (statuses != null)
            {
                List<HRT.DTO.ProposalStep> content = new List<HRT.DTO.ProposalStep>();
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
