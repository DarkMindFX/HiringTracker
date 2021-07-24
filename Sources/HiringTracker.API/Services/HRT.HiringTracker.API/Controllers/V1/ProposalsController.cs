


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
    public class ProposalsController : BaseController
    {
        private readonly Dal.IProposalDal _dalProposal;
        private readonly ILogger<ProposalsController> _logger;


        public ProposalsController( Dal.IProposalDal dalProposal,
                                    ILogger<ProposalsController> logger)
        {
            _dalProposal = dalProposal; 
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalProposal.GetAll();

            IList<DTO.Proposal> dtos = new List<DTO.Proposal>();

            foreach (var p in entities)
            {
                var dto = ProposalConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("{id}"), ActionName("GetProposal")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalProposal.Get(id);
            if (entity != null)
            {
                var dto = ProposalConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"Proposal was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpDelete("{id}"), ActionName("DeleteProposal")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalProposal.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalProposal.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete Proposal [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"Proposal not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpPost, ActionName("InsertProposal")]
        public IActionResult Insert(DTO.Proposal dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = ProposalConvertor.Convert(dto);

            Proposal newEntity = _dalProposal.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, ProposalConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        [Authorize]
        [HttpPut, ActionName("UpdateProposal")]
        public IActionResult Update(DTO.Proposal dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = ProposalConvertor.Convert(dto);

            var existingEntity = _dalProposal.Get(newEntity.ID);
            if (existingEntity != null)
            {
                Proposal entity = _dalProposal.Update(newEntity);

                response = Ok(ProposalConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"Proposal not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

