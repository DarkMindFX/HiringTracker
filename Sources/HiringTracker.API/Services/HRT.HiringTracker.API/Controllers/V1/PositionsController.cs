using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRT.HiringTracker.API.Filters;
using HRT.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRT.HiringTracker.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionDal _dalPosition;
        private readonly IPositionStatusDal _dalPositionStatus;
        private readonly IUserDal _dalUser;
        private readonly ILogger<PositionsController> _logger;

        public PositionsController(IPositionDal dalPosition, 
                                    IPositionStatusDal dalPositionStatus,
                                    IUserDal dalUser,
                                    ILogger<PositionsController> logger)
        {
            _dalPosition = dalPosition;
            _dalPositionStatus = dalPositionStatus;
            _dalUser = dalUser;
            _logger = logger;
        }
    }
}
