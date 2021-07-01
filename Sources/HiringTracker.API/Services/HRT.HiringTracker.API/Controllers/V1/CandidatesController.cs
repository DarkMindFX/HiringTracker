


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
    public class CandidatesController : BaseController
    {
        private readonly Dal.ICandidateDal _dalCandidate;
        private readonly ILogger<CandidatesController> _logger;


        public CandidatesController( Dal.ICandidateDal dalCandidate,
                                    ILogger<CandidatesController> logger)
        {
            _dalCandidate = dalCandidate; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalCandidate.GetAll();

            IList<DTO.Candidate> dtos = new List<DTO.Candidate>();

            foreach (var p in entities)
            {
                var dto = CandidateConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetCandidate")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalCandidate.Get(id);
            if (entity != null)
            {
                var dto = CandidateConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"Candidate was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteCandidate")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalCandidate.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalCandidate.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete Candidate [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"Candidate not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertCandidate")]
        public IActionResult Insert(DTO.Candidate dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = CandidateConvertor.Convert(dto);

            Candidate newEntity = _dalCandidate.Insert(entity);

            response = Ok(CandidateConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateCandidate")]
        public IActionResult Update(DTO.Candidate dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = CandidateConvertor.Convert(dto);

            var existingEntity = _dalCandidate.Get(newEntity.ID);
            if (existingEntity != null)
            {
                Candidate entity = _dalCandidate.Update(newEntity);

                response = Ok(CandidateConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"Candidate not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

