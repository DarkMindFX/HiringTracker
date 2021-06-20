using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HRT.HiringTracker.API.Filters;
using HRT.HiringTracker.API.Helpers;
using HRT.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRT.HiringTracker.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class SkillProficienciesController : ControllerBase
    {
        private readonly Dal.ISkillProficiencyDal _dalSkillProficiency;
        private readonly ILogger<PositionStatusesController> _logger;

        public SkillProficienciesController(ILogger<PositionStatusesController> logger,
                                            Dal.ISkillProficiencyDal dalSkillProficiency)
        {
            _logger = logger;
            _dalSkillProficiency = dalSkillProficiency;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetProficiencies()
        {
            IActionResult response = null;

            var statuses = _dalSkillProficiency.GetAll();
            if (statuses != null)
            {
                List<HRT.DTO.SkillProficiency> content = new List<HRT.DTO.SkillProficiency>();
                foreach (var u in statuses)
                {
                    content.Add(EntityToDtoConvertor.Convert(u, this.Url));
                }

                response = Ok(content);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.InternalServerError, "Failed to obtain list of Skill Proficiencies");
            }

            return response;
        }
    }
}
