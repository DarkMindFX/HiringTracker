


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
    public class ProposalCommentsController : BaseController
    {
        private readonly Dal.IProposalCommentDal _dalProposalComment;
        private readonly ILogger<ProposalCommentsController> _logger;


        public ProposalCommentsController( Dal.IProposalCommentDal dalProposalComment,
                                    ILogger<ProposalCommentsController> logger)
        {
            _dalProposalComment = dalProposalComment; 
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalProposalComment.GetAll();

            IList<DTO.ProposalComment> dtos = new List<DTO.ProposalComment>();

            foreach (var p in entities)
            {
                var dto = ProposalCommentConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("{proposalid}/{commentid}"), ActionName("GetProposalComment")]
        public IActionResult Get(System.Int64 proposalid, System.Int64 commentid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalProposalComment.Get(proposalid, commentid);
            if (entity != null)
            {
                var dto = ProposalCommentConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"ProposalComment was not found [ids:{proposalid}, {commentid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpDelete("{proposalid}/{commentid}"), ActionName("DeleteProposalComment")]
        public IActionResult Delete(System.Int64 proposalid, System.Int64 commentid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalProposalComment.Get(proposalid, commentid);

            if (existingEntity != null)
            {
                bool removed = _dalProposalComment.Delete(proposalid, commentid);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete ProposalComment [ids:{proposalid}, {commentid}]");
                }
            }
            else
            {
                response = NotFound($"ProposalComment not found [ids:{proposalid}, {commentid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpPost, ActionName("InsertProposalComment")]
        public IActionResult Insert(DTO.ProposalComment dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = ProposalCommentConvertor.Convert(dto);

            ProposalComment newEntity = _dalProposalComment.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, ProposalCommentConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        [Authorize]
        [HttpPut, ActionName("UpdateProposalComment")]
        public IActionResult Update(DTO.ProposalComment dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = ProposalCommentConvertor.Convert(dto);

            var existingEntity = _dalProposalComment.Get(newEntity.ProposalID, newEntity.CommentID);
            if (existingEntity != null)
            {
                ProposalComment entity = _dalProposalComment.Update(newEntity);

                response = Ok(ProposalCommentConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"ProposalComment not found [ids:{newEntity.ProposalID}, {newEntity.CommentID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

