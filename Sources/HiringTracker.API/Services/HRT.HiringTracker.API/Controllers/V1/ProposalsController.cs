using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRT.HiringTracker.API.Dal;
using HRT.HiringTracker.API.Filters;
using HRT.HiringTracker.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRT.HiringTracker.API.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class ProposalsController : ControllerBase
    {
        private readonly ILogger<ProposalsController> _logger;
        private readonly IProposalDal _dalProposal;
        private readonly Dal.ICandidateDal _dalCandidate;
        private readonly Dal.IProposalStatusDal _dalPropStatus;
        private readonly Dal.IProposalStepDal _dalPropStep;
        private readonly Dal.IUserDal _dalUser;
        private readonly Dal.IPositionDal _dalPosition;
        private readonly Dal.IPositionStatusDal _dalPosStatus;

        public ProposalsController(ILogger<ProposalsController> logger,
                                   Dal.IProposalDal dalProposal,
                                   Dal.ICandidateDal dalCandidate,
                                   Dal.IProposalStatusDal dalPropStatus,
                                   Dal.IProposalStepDal dalPropStep,
                                   Dal.IUserDal dalUser,
                                   Dal.IPositionDal dalPosition,
                                   Dal.IPositionStatusDal dalPosStatus)
        {
            _logger = logger;
            _dalProposal = dalProposal;
            _dalCandidate = dalCandidate;
            _dalPropStatus = dalPropStatus;
            _dalPropStep = dalPropStep;
            _dalUser = dalUser;
            _dalPosition = dalPosition;
            _dalPosStatus = dalPosStatus;

        }

        [Authorize]
        [HttpGet]
        public IActionResult GetProposals()
        {
            IActionResult response = null;

            var entities = _dalProposal.GetAll();

            var dtos = new List<DTO.Proposal>();

            var candidates = _dalCandidate.GetAllAsDictionary();
            var statuses = _dalPropStatus.GetAllAsDictionary();
            var steps = _dalPropStep.GetAllAsDictionary();
            var users = _dalUser.GetAllAsDictionary();
            var positions = _dalPosition.GetAllAsDictionary();
            var posStatuses = _dalPosStatus.GetAllAsDictionary();

            foreach (var e in entities)
            {
                dtos.Add(EntityToDtoConvertor.Convert(e, candidates, statuses, steps, users, positions, posStatuses, this.Url));
            }

            response = Ok(dtos);

            return response;
        }

        [Authorize]
        [HttpGet("{id}"), ActionName("GetProposal")]
        public IActionResult GetProposal(long id)
        {
            IActionResult response = null;

            var entity = _dalProposal.Get(id);
            var candidates = _dalCandidate.GetAllAsDictionary();
            var statuses = _dalPropStatus.GetAllAsDictionary();
            var steps = _dalPropStep.GetAllAsDictionary();
            var users = _dalUser.GetAllAsDictionary();
            var positions = _dalPosition.GetAllAsDictionary();
            var posStatuses = _dalPosStatus.GetAllAsDictionary();

            if (entity != null)
            {
                var dto = EntityToDtoConvertor.Convert(entity, candidates,  statuses, steps, users, positions, posStatuses, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = NotFound($"Proposal with given ID was not found [id:{id}]");
            }

            return response;
        }

        [Authorize]
        [HttpDelete("{id}"), ActionName("DeleteProposal")]
        public IActionResult DeleteProposal(long id)
        {
            IActionResult response = null;

            var entity = _dalProposal.Get(id);

            if (entity != null)
            {
                bool removed = _dalProposal.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    throw new InvalidOperationException($"Failed to remove user [id:{id}]");
                }
            }
            else
            {
                response = NotFound($"Proposal with given ID was not found [id:{id}]");
            }

            return response;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddProposal(DTO.Proposal dto)
        {
            IActionResult response = UpsertProposal(dto);

            return response;
        }


        [Authorize]
        [HttpPut, ActionName("UpdateProposal")]
        public IActionResult UpsertProposal(DTO.Proposal dto)
        {
            IActionResult response = null;

            return response;
        }

    }
}
