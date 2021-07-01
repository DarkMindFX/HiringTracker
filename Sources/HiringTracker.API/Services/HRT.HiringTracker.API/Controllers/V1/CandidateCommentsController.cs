


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
    public class CandidateCommentsController : BaseController
    {
        private readonly Dal.ICandidateCommentDal _dalCandidateComment;
        private readonly ILogger<CandidateCommentsController> _logger;


        public CandidateCommentsController( Dal.ICandidateCommentDal dalCandidateComment,
                                    ILogger<CandidateCommentsController> logger)
        {
            _dalCandidateComment = dalCandidateComment; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalCandidateComment.GetAll();

            IList<DTO.CandidateComment> dtos = new List<DTO.CandidateComment>();

            foreach (var p in entities)
            {
                var dto = CandidateCommentConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{candidateid}/{commentid}"), ActionName("GetCandidateComment")]
        public IActionResult Get(System.Int64 candidateid, System.Int64 commentid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalCandidateComment.Get(candidateid, commentid);
            if (entity != null)
            {
                var dto = CandidateCommentConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"CandidateComment was not found [ids:{candidateid}, {commentid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{candidateid}/{commentid}"), ActionName("DeleteCandidateComment")]
        public IActionResult Delete(System.Int64 candidateid, System.Int64 commentid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalCandidateComment.Get(candidateid, commentid);

            if (existingEntity != null)
            {
                bool removed = _dalCandidateComment.Delete(candidateid, commentid);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete CandidateComment [ids:{candidateid}, {commentid}]");
                }
            }
            else
            {
                response = NotFound($"CandidateComment not found [ids:{candidateid}, {commentid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertCandidateComment")]
        public IActionResult Insert(DTO.CandidateComment dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = CandidateCommentConvertor.Convert(dto);

            CandidateComment newEntity = _dalCandidateComment.Insert(entity);

            response = Ok(CandidateCommentConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateCandidateComment")]
        public IActionResult Update(DTO.CandidateComment dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = CandidateCommentConvertor.Convert(dto);

            var existingEntity = _dalCandidateComment.Get(newEntity.CandidateID, newEntity.CommentID);
            if (existingEntity != null)
            {
                CandidateComment entity = _dalCandidateComment.Update(newEntity);

                response = Ok(CandidateCommentConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"CandidateComment not found [ids:{newEntity.CandidateID}, {newEntity.CommentID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

