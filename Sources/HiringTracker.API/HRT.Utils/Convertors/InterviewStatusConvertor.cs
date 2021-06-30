




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class InterviewStatusConvertor
    {
        public static DTO.InterviewStatus Convert(Interfaces.Entities.InterviewStatus entity, IUrlHelper url)
        {
            var dto = new DTO.InterviewStatus()
            {
        		        ID = entity.ID,

				        Name = entity.Name,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetInterviewStatus", "interviewstatuss", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteInterviewStatus", "interviewstatuss", new { id = dto.ID  }), "delete_interviewstatus", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertInterviewStatus", "interviewstatuss"), "insert_interviewstatus", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateInterviewStatus", "interviewstatuss"), "update_interviewstatus", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.InterviewStatus Convert(DTO.InterviewStatus dto)
        {
            var entity = new Interfaces.Entities.InterviewStatus()
            {
                
        		        ID = dto.ID,

				        Name = dto.Name,

				
     
            };

            return entity;
        }
    }
}
