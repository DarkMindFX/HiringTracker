




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class InterviewConvertor
    {
        public static DTO.Interview Convert(Interfaces.Entities.Interview entity, IUrlHelper url)
        {
            var dto = new DTO.Interview()
            {
        		        ID = entity.ID,

				        ProposalID = entity.ProposalID,

				        InterviewTypeID = entity.InterviewTypeID,

				        StartTime = entity.StartTime,

				        EndTime = entity.EndTime,

				        InterviewStatusID = entity.InterviewStatusID,

				        CreatedByID = entity.CreatedByID,

				        CretedDate = entity.CretedDate,

				        ModifiedByID = entity.ModifiedByID,

				        ModifiedDate = entity.ModifiedDate,

				
            };

            
            dto.Links.Add(new DTO.Link(url.Action("GetInterview", "interviews", new { id = dto.ID  }), "self", "GET"));
            dto.Links.Add(new DTO.Link(url.Action("DeleteInterview", "interviews", new { id = dto.ID  }), "delete_interview", "DELETE"));
            dto.Links.Add(new DTO.Link(url.Action("InsertInterview", "interviews"), "insert_interview", "POST"));
            dto.Links.Add(new DTO.Link(url.Action("UpdateInterview", "interviews"), "update_interview", "PUT"));

            return dto;

        }

        public static Interfaces.Entities.Interview Convert(DTO.Interview dto)
        {
            var entity = new Interfaces.Entities.Interview()
            {
                
        		        ID = dto.ID,

				        ProposalID = dto.ProposalID,

				        InterviewTypeID = dto.InterviewTypeID,

				        StartTime = dto.StartTime,

				        EndTime = dto.EndTime,

				        InterviewStatusID = dto.InterviewStatusID,

				        CreatedByID = dto.CreatedByID,

				        CretedDate = dto.CretedDate,

				        ModifiedByID = dto.ModifiedByID,

				        ModifiedDate = dto.ModifiedDate,

				
     
            };

            return entity;
        }
    }
}
