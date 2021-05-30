using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRT.HiringTracker.API.Dal;
using HRT.HiringTracker.API.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRT.HiringTracker.API.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class ProposalsController : ControllerBase
    {
        private readonly IPositionCandidateDal _dalPosCandidate;

        public ProposalsController(ILogger<ProposalsController> logger,
                                   Dal.IPositionCandidateDal dalPosCandidate)
        {
            _dalPosCandidate = dalPosCandidate;
        }
    }
}
