using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HRT.HiringTracker.API.Filters;
using HRT.HiringTracker.API.Helpers;
using HRT.Interfaces;
using HRT.Interfaces.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRT.HiringTracker.API.Controllers.V1
{
    [ApiController]
    [UnhandledExceptionFilter]
    [Route("api/v1/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly Dal.ISkillDal _dalSkill;
        private readonly ILogger<SkillsController> _logger;

        public SkillsController(ILogger<SkillsController> logger,
                                Dal.ISkillDal dalSkill)
        {
            _logger = logger;
            _dalSkill = dalSkill;
        }

        [HttpGet]
        public IActionResult GetSkills()
        {
            IActionResult response = null;

            var statuses = _dalSkill.GetAll();
            if (statuses != null)
            {
                List<HRT.DTO.Skill> content = new List<HRT.DTO.Skill>();
                foreach (var u in statuses)
                {
                    content.Add(EntityToDtoConvertor.Convert(u, this.Url));
                }

                response = Ok(content);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.InternalServerError, "Failed to obtain list of Skills");
            }

            return response;
        }

        [HttpDelete("{id}"), ActionName("DeleteSkill")]
        public IActionResult DeleteSkill(long id)
        {
            IActionResult response = null;

            if (_dalSkill.Delete(id))
            {
                response = Ok();
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete skill [id:{id}]");
            }

            return response;
        }

        [HttpPut, ActionName("UpdateSkill")]
        public IActionResult UpdateSkill(DTO.Skill dto)
        {
            IActionResult response = null;

            User editor = HttpContext.Items["User"] as User;

            var entity = EntityToDtoConvertor.Convert(dto);

            long? id = _dalSkill.Upsert(entity, editor.ID);

            response = Ok();

            return response;
        }

        [HttpPost, ActionName("InsertSkill")]
        public IActionResult InsertSkill(DTO.Skill dto)
        {
            IActionResult response = null;

            User editor = HttpContext.Items["User"] as User;

            var entity = EntityToDtoConvertor.Convert(dto);

            long? id = _dalSkill.Upsert(entity, editor.ID);

            if(id != null)
            {
                dto.ID = (long)id;
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to insert skill");
            }

            return response;
        }
    }
}
