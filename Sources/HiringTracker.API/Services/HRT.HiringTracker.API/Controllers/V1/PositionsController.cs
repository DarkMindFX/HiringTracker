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
    [Route("api/v1/[controller]")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class PositionsController : ControllerBase
    {
        private readonly Dal.IPositionDal _dalPosition;
        private readonly Dal.IPositionStatusDal _dalPositionStatus;
        private readonly Dal.IUserDal _dalUser;
        private readonly Dal.ISkillDal _dalSkill;
        private readonly Dal.ISkillProficiencyDal _dalSkillProfs;
        private readonly ILogger<PositionsController> _logger;


        public PositionsController(Dal.IPositionDal dalPosition,
                                    Dal.IPositionStatusDal dalPositionStatus,
                                    Dal.IUserDal dalUser,
                                    Dal.ISkillDal dalSkill,
                                    Dal.ISkillProficiencyDal dalSkillProfs,
                                    ILogger<PositionsController> logger)
        {
            _dalPosition = dalPosition;
            _dalPositionStatus = dalPositionStatus;
            _dalUser = dalUser;
            _dalSkill = dalSkill;
            _dalSkillProfs = dalSkillProfs;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetPositions()
        {
            IActionResult response = null;

            var positions = _dalPosition.GetAll();
            var statuses = _dalPositionStatus.GetAllAsDictionary();
            var users = _dalUser.GetAllAsDictionary();


            IList<DTO.Position> dtos = new List<DTO.Position>();

            foreach (var p in positions)
            {
                var dto = EntityToDtoConvertor.Convert(p, statuses, users, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            return response;
        }

        [Authorize]
        [HttpGet("{id}"), ActionName("GetPosition")]
        public IActionResult GetPosition(long id)
        {
            IActionResult response = null;
            var statuses = _dalPositionStatus.GetAllAsDictionary();
            var users = _dalUser.GetAllAsDictionary();

            var entity = _dalPosition.Get(id);
            if (entity != null)
            {
                var dto = EntityToDtoConvertor.Convert(entity, statuses, users, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"Position was not found [id:{id}]");
            }

            return response;
        }

        [Authorize]
        [HttpDelete("{id}"), ActionName("DeletePosition")]
        public IActionResult DeletePosition(long id)
        {
            IActionResult response = null;

            bool removed = _dalPosition.Delete(id);
            if(removed)
            {
                response = Ok();
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete position [id:{id}]");
            }

            return response;
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddPosition(DTO.PositionUpsert dto)
        {
            IActionResult response = UpsertPosition(dto);

            return response;
        }


        [Authorize]
        [HttpPut, ActionName("UpdatePosition")]
        public IActionResult UpsertPosition(DTO.PositionUpsert dto)
        {
            IActionResult response = null;

            var entity = EntityToDtoConvertor.Convert(dto.Position);

            User editor = HttpContext.Items["User"] as User;

            long? id = _dalPosition.Upsert(entity, editor != null ? editor.ID : null);

            if (dto.Position.ID != null || id != null)
            {
                var posSkills = new List<PositionSkill>();
                foreach (var s in dto.Skills)
                {
                    posSkills.Add(EntityToDtoConvertor.Convert(s));
                }

                _dalPosition.SetSkills(dto.Position.ID ?? (long)id, posSkills);
            }

            response = Ok( new DTO.PositionUpsertResponse() {  PositionID = dto.Position.ID ?? (long)id } );

            return response;
        }

        [Authorize]
        [HttpGet("{id}/skills"), ActionName("GetPositionSkills")]
        public IActionResult GetPositionSkills(long id)
        {
            IActionResult response = null;

            var entity = _dalPosition.Get(id);
            if (entity != null)
            {
                var entities = _dalPosition.GetSkills(id);
                var dtos = new List<DTO.PositionSkill>();

                var skills = _dalSkill.GetAllAsDictionary();
                var profs = _dalSkillProfs.GetAllAsDictionary();

                foreach(var e in entities)
                {
                    var dto = EntityToDtoConvertor.Convert(e, skills, profs, this.Url);
                    dtos.Add(dto);
                }

                response = Ok(dtos);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"Position was not found [id:{id}]");
            }

            return response;
        }
    }

}
