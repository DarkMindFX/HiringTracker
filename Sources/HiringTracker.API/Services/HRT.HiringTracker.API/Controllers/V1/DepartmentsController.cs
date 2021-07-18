


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
    public class DepartmentsController : BaseController
    {
        private readonly Dal.IDepartmentDal _dalDepartment;
        private readonly ILogger<DepartmentsController> _logger;


        public DepartmentsController( Dal.IDepartmentDal dalDepartment,
                                    ILogger<DepartmentsController> logger)
        {
            _dalDepartment = dalDepartment; 
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalDepartment.GetAll();

            IList<DTO.Department> dtos = new List<DTO.Department>();

            foreach (var p in entities)
            {
                var dto = DepartmentConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("{id}"), ActionName("GetDepartment")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalDepartment.Get(id);
            if (entity != null)
            {
                var dto = DepartmentConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"Department was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpDelete("{id}"), ActionName("DeleteDepartment")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalDepartment.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalDepartment.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete Department [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"Department not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpPost, ActionName("InsertDepartment")]
        public IActionResult Insert(DTO.Department dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = DepartmentConvertor.Convert(dto);

            Department newEntity = _dalDepartment.Insert(entity);

            response = Ok(DepartmentConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        [Authorize]
        [HttpPut, ActionName("UpdateDepartment")]
        public IActionResult Update(DTO.Department dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = DepartmentConvertor.Convert(dto);

            var existingEntity = _dalDepartment.Get(newEntity.ID);
            if (existingEntity != null)
            {
                Department entity = _dalDepartment.Update(newEntity);

                response = Ok(DepartmentConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"Department not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

