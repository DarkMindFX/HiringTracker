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
    public class PositionCandidateStatusesController : ControllerBase
    {

        private readonly ILogger<PositionCandidateStatusesController> _logger;
        private readonly IPositionCandidateStatusDal _dalPosCandidateStatus;

        public PositionCandidateStatusesController(ILogger<PositionCandidateStatusesController> logger,
                                IPositionCandidateStatusDal dalPosCandidateStatus)
        {
            _logger = logger;
            _dalPosCandidateStatus = dalPosCandidateStatus;
            
        }

        [HttpGet]
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
