


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HRT.HiringTracker.API.Filters;
using HRT.Interfaces.Entities;
using HRT.Utils.Convertors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace HRT.HiringTracker.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class PositionsController : BaseController
    {
        private readonly Dal.IPositionDal _dalPosition;
        private readonly ILogger<PositionsController> _logger;


        public PositionsController( Dal.IPositionDal dalPosition,
                                    ILogger<PositionsController> logger)
        {
            _dalPosition = dalPosition; 
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalPosition.GetAll();

            IList<DTO.Position> dtos = new List<DTO.Position>();

            foreach (var p in entities)
            {
                var dto = PositionConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("{id}"), ActionName("GetPosition")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalPosition.Get(id);
            if (entity != null)
            {
                var dto = PositionConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"Position was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpDelete("{id}"), ActionName("DeletePosition")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalPosition.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalPosition.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete Position [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"Position not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpPost, ActionName("InsertPosition")]
        public IActionResult Insert(DTO.Position dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = PositionConvertor.Convert(dto);
            entity.CreatedDate = DateTime.Now;
            entity.CreatedByID = (long)base.CurrentUser.ID;

            Position newEntity = _dalPosition.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, PositionConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        [Authorize]
        [HttpPut, ActionName("UpdatePosition")]
        public IActionResult Update(DTO.Position dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = PositionConvertor.Convert(dto);

            var existingEntity = _dalPosition.Get(newEntity.ID);
            if (existingEntity != null)
            {
                newEntity.ModifiedDate = DateTime.Now;
                newEntity.ModifiedByID = (long)base.CurrentUser.ID;
                Position entity = _dalPosition.Update(newEntity);

                response = Ok(PositionConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"Position not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

