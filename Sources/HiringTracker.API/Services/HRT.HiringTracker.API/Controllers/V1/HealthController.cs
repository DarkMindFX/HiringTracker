using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HRT.HiringTracker.API.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRT.HiringTracker.API.Controllers.V1
{
    [ApiController]
    [UnhandledExceptionFilter]
    [Route("api/v1/[controller]")]
    
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var dto = new DTO.HealthResponse();
            dto.Message = "HRT.HiringTracker.API OK";

            IActionResult response = StatusCode((int)HttpStatusCode.OK, dto);

            return response;
        }
    }
}
