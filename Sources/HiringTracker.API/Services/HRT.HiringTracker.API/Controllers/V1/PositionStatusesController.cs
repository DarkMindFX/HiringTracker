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

    public class PositionStatusesController : ControllerBase
    {
        private readonly ILogger<PositionStatusesController> _logger;
        private readonly IPositionStatusDal _dalPosStatus;
        
        public PositionStatusesController(  ILogger<PositionStatusesController> logger,
                                            IPositionStatusDal dalPosStatus)
        {
            _logger = logger;
            _dalPosStatus = dalPosStatus;            
        }

        [HttpGet]
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

                response = Ok(content);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.InternalServerError, "Failed to obtain list of Position Statuses");
            }

            return response;
        }
    }
}
