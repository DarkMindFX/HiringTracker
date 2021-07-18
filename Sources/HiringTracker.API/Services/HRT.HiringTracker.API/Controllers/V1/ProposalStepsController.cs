


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
    public class ProposalStepsController : BaseController
    {
        private readonly Dal.IProposalStepDal _dalProposalStep;
        private readonly ILogger<ProposalStepsController> _logger;


        public ProposalStepsController( Dal.IProposalStepDal dalProposalStep,
                                    ILogger<ProposalStepsController> logger)
        {
            _dalProposalStep = dalProposalStep; 
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalProposalStep.GetAll();

            IList<DTO.ProposalStep> dtos = new List<DTO.ProposalStep>();

            foreach (var p in entities)
            {
                var dto = ProposalStepConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("{id}"), ActionName("GetProposalStep")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalProposalStep.Get(id);
            if (entity != null)
            {
                var dto = ProposalStepConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"ProposalStep was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpDelete("{id}"), ActionName("DeleteProposalStep")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalProposalStep.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalProposalStep.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete ProposalStep [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"ProposalStep not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpPost, ActionName("InsertProposalStep")]
        public IActionResult Insert(DTO.ProposalStep dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = ProposalStepConvertor.Convert(dto);

            ProposalStep newEntity = _dalProposalStep.Insert(entity);

            response = Ok(ProposalStepConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        [Authorize]
        [HttpPut, ActionName("UpdateProposalStep")]
        public IActionResult Update(DTO.ProposalStep dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = ProposalStepConvertor.Convert(dto);

            var existingEntity = _dalProposalStep.Get(newEntity.ID);
            if (existingEntity != null)
            {
                ProposalStep entity = _dalProposalStep.Update(newEntity);

                response = Ok(ProposalStepConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"ProposalStep not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

