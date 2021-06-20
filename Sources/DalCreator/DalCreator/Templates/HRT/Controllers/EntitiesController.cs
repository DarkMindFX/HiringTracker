using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HRT.HiringTracker.API.Filters;
using HRT.HiringTracker.API.Helpers;
using HRT.Interfaces;
using HRT.Interfaces.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRT.HiringTracker.API.Controllers.V1
{
    [ApiController]
    [UnhandledExceptionFilter]
    [Route("api/v1/[controller]")]
    public class {Entities}Controller : ControllerBase
    {
        private readonly Dal.I{Entity}Dal _dal{Entity};
        private readonly ILogger<{Entity}sController> _logger;
        {DALS_LIST_FIELDS}

        public {Entity}sController(ILogger<{Entity}sController> logger,
                                Dal.I{Entity}Dal dal{Entity}
                                {DALS_LIST_PARAMS})
        {
            _logger = logger;
            _dal{Entity} = dal{Entity};
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get{Entity}s()
        {
            IActionResult response = null;

            var statuses = _dal{Entity}.GetAll();
            if (statuses != null)
            {
                List<HRT.DTO.{Entity}> content = new List<HRT.DTO.{Entity}>();
                foreach (var u in statuses)
                {
                    content.Add(EntityToDtoConvertor.Convert(u, this.Url));
                }

                response = Ok(content);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.InternalServerError, "Failed to obtain list of {Entities}");
            }

            return response;
        }

        [Authorize]
        [HttpDelete("{id}"), ActionName("Delete{Entity}")]
        public IActionResult Delete{Entity}(long id)
        {
            IActionResult response = null;

            if (_dal{Entity}.Delete(id))
            {
                response = Ok();
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete {Entity} [id:{id}]");
            }

            return response;
        }

        [Authorize]
        [HttpPut, ActionName("Update{Entity}")]
        public IActionResult Update{Entity}(DTO.{Entity} dto)
        {
            IActionResult response = null;

            User editor = HttpContext.Items["User"] as User;

            var entity = EntityToDtoConvertor.Convert(dto);

            long? id = _dal{Entity}.Upsert(entity, editor.ID);

            response = Ok();

            return response;
        }

        [Authorize]
        [HttpPost, ActionName("Insert{Entity}")]
        public IActionResult Insert{Entity}(DTO.{Entity} dto)
        {
            IActionResult response = null;

            User editor = HttpContext.Items["User"] as User;

            var entity = EntityToDtoConvertor.Convert(dto);

            long? id = _dal{Entity}.Upsert(entity, editor.ID);

            if (id != null)
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
