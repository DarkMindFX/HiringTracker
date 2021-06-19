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
    public class ProposalStatusesController : ControllerBase
    {

        private readonly ILogger<ProposalStatusesController> _logger;
        private readonly Dal.IProposalStatusDal _dalPosCandidateStatus;

        public ProposalStatusesController( ILogger<ProposalStatusesController> logger,
                                                    Dal.IProposalStatusDal dalPosCandidateStatus)
        {
            _logger = logger;
            _dalPosCandidateStatus = dalPosCandidateStatus;
            
        }

        [HttpGet]
        public IActionResult GetProposalStatuses()
        {
            IActionResult response = null;

            var statuses = _dalPosCandidateStatus.GetAll();
            if (statuses != null)
            {
                List<HRT.DTO.ProposalStatus> content = new List<HRT.DTO.ProposalStatus>();
                foreach (var u in statuses)
                {
                    content.Add(EntityToDtoConvertor.Convert(u, this.Url));
                }

                response = Ok(content);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.InternalServerError, "Failed to obtain list of Position Candidate Statuses");
            }

            return response;
        }
    }
}
