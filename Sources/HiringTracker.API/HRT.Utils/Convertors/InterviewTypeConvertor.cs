




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class InterviewTypeConvertor
    {
        public static DTO.InterviewType Convert(Interfaces.Entities.InterviewType entity, IUrlHelper url)
        {
            var dto = new DTO.InterviewType()
            {
        		        ID = entity.ID,

				        Name = entity.Name,

				
            };

            
            dto.Links.Add(new DTO.Link(url.Action("GetInterviewType", "interviewtypes", new { id = dto.ID  }), "self", "GET"));
            dto.Links.Add(new DTO.Link(url.Action("DeleteInterviewType", "interviewtypes", new { id = dto.ID  }), "delete_interviewtype", "DELETE"));
            dto.Links.Add(new DTO.Link(url.Action("InsertInterviewType", "interviewtypes"), "insert_interviewtype", "POST"));
            dto.Links.Add(new DTO.Link(url.Action("UpdateInterviewType", "interviewtypes"), "update_interviewtype", "PUT"));

            return dto;

        }

        public static Interfaces.Entities.InterviewType Convert(DTO.InterviewType dto)
        {
            var entity = new Interfaces.Entities.InterviewType()
            {
                
        		        ID = dto.ID,

				        Name = dto.Name,

				
     
            };

            return entity;
        }
    }
}
