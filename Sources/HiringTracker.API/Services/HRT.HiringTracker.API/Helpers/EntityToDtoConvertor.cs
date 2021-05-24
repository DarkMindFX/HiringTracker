using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRT.HiringTracker.API.Helpers
{
    public static class EntityToDtoConvertor
    {
        public static DTO.PositionStatus Convert(Interfaces.Entities.PositionStatus entity, IUrlHelper url)
        {
            var dto = new DTO.PositionStatus()
            {
                Name = entity.Name,
                StatusID = entity.StatusID
            };

            return dto;
        }

        public static DTO.PositionCandidateStatus Convert(Interfaces.Entities.PositionCandidateStatus entity, IUrlHelper url)
        {
            var dto = new DTO.PositionCandidateStatus()
            {
                Name = entity.Name,
                StatusID = entity.StatusID
            };

            return dto;
        }

        public static DTO.PositionCandidateStep Convert(Interfaces.Entities.PositionCandidateStep entity, IUrlHelper url)
        {
            var dto = new DTO.PositionCandidateStep()
            {
                Name = entity.Name,
                StepID = entity.StepID,
                ReqDueDate = entity.ReqDueDate,
                RequiresRespInDays = entity.RequiresRespInDays
            };

            return dto;
        }
    }
}
