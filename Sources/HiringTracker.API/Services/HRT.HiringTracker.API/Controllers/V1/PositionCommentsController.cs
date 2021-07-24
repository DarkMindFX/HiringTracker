


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
    public class PositionCommentsController : BaseController
    {
        private readonly Dal.IPositionCommentDal _dalPositionComment;
        private readonly ILogger<PositionCommentsController> _logger;


        public PositionCommentsController( Dal.IPositionCommentDal dalPositionComment,
                                    ILogger<PositionCommentsController> logger)
        {
            _dalPositionComment = dalPositionComment; 
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalPositionComment.GetAll();

            IList<DTO.PositionComment> dtos = new List<DTO.PositionComment>();

            foreach (var p in entities)
            {
                var dto = PositionCommentConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("{positionid}/{commentid}"), ActionName("GetPositionComment")]
        public IActionResult Get(System.Int64 positionid, System.Int64 commentid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalPositionComment.Get(positionid, commentid);
            if (entity != null)
            {
                var dto = PositionCommentConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"PositionComment was not found [ids:{positionid}, {commentid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpDelete("{positionid}/{commentid}"), ActionName("DeletePositionComment")]
        public IActionResult Delete(System.Int64 positionid, System.Int64 commentid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalPositionComment.Get(positionid, commentid);

            if (existingEntity != null)
            {
                bool removed = _dalPositionComment.Delete(positionid, commentid);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete PositionComment [ids:{positionid}, {commentid}]");
                }
            }
            else
            {
                response = NotFound($"PositionComment not found [ids:{positionid}, {commentid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpPost, ActionName("InsertPositionComment")]
        public IActionResult Insert(DTO.PositionComment dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = PositionCommentConvertor.Convert(dto);

            PositionComment newEntity = _dalPositionComment.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, PositionCommentConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        [Authorize]
        [HttpPut, ActionName("UpdatePositionComment")]
        public IActionResult Update(DTO.PositionComment dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = PositionCommentConvertor.Convert(dto);

            var existingEntity = _dalPositionComment.Get(newEntity.PositionID, newEntity.CommentID);
            if (existingEntity != null)
            {
                PositionComment entity = _dalPositionComment.Update(newEntity);

                response = Ok(PositionCommentConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"PositionComment not found [ids:{newEntity.PositionID}, {newEntity.CommentID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

