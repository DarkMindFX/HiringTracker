


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HRT.HiringTracker.API.Filters;
using HRT.Interfaces.Entities;
using HRT.Utils.Convertors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace HRT.HiringTracker.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class PositionStatusesController : BaseController
    {
        private readonly Dal.IPositionStatusDal _dalPositionStatus;
        private readonly ILogger<PositionStatusesController> _logger;


        public PositionStatusesController( Dal.IPositionStatusDal dalPositionStatus,
                                    ILogger<PositionStatusesController> logger)
        {
            _dalPositionStatus = dalPositionStatus; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalPositionStatus.GetAll();

            IList<DTO.PositionStatus> dtos = new List<DTO.PositionStatus>();

            foreach (var p in entities)
            {
                var dto = PositionStatusConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetPositionStatus")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalPositionStatus.Get(id);
            if (entity != null)
            {
                var dto = PositionStatusConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"PositionStatus was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeletePositionStatus")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalPositionStatus.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalPositionStatus.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete PositionStatus [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"PositionStatus not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertPositionStatus")]
        public IActionResult Insert(DTO.PositionStatus dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = PositionStatusConvertor.Convert(dto);

            PositionStatus newEntity = _dalPositionStatus.Insert(entity);

            response = Ok(PositionStatusConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdatePositionStatus")]
        public IActionResult Update(DTO.PositionStatus dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = PositionStatusConvertor.Convert(dto);

            var existingEntity = _dalPositionStatus.Get(newEntity.ID);
            if (existingEntity != null)
            {
                PositionStatus entity = _dalPositionStatus.Update(newEntity);

                response = Ok(PositionStatusConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"PositionStatus not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

