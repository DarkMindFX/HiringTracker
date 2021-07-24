


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
    public class ProposalStatusesController : BaseController
    {
        private readonly Dal.IProposalStatusDal _dalProposalStatus;
        private readonly ILogger<ProposalStatusesController> _logger;


        public ProposalStatusesController( Dal.IProposalStatusDal dalProposalStatus,
                                    ILogger<ProposalStatusesController> logger)
        {
            _dalProposalStatus = dalProposalStatus; 
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalProposalStatus.GetAll();

            IList<DTO.ProposalStatus> dtos = new List<DTO.ProposalStatus>();

            foreach (var p in entities)
            {
                var dto = ProposalStatusConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("{id}"), ActionName("GetProposalStatus")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalProposalStatus.Get(id);
            if (entity != null)
            {
                var dto = ProposalStatusConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"ProposalStatus was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpDelete("{id}"), ActionName("DeleteProposalStatus")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalProposalStatus.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalProposalStatus.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete ProposalStatus [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"ProposalStatus not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpPost, ActionName("InsertProposalStatus")]
        public IActionResult Insert(DTO.ProposalStatus dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = ProposalStatusConvertor.Convert(dto);

            ProposalStatus newEntity = _dalProposalStatus.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, ProposalStatusConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        [Authorize]
        [HttpPut, ActionName("UpdateProposalStatus")]
        public IActionResult Update(DTO.ProposalStatus dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = ProposalStatusConvertor.Convert(dto);

            var existingEntity = _dalProposalStatus.Get(newEntity.ID);
            if (existingEntity != null)
            {
                ProposalStatus entity = _dalProposalStatus.Update(newEntity);

                response = Ok(ProposalStatusConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"ProposalStatus not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

