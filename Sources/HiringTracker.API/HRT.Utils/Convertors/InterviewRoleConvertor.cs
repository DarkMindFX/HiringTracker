




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class InterviewRoleConvertor
    {
        public static DTO.InterviewRole Convert(Interfaces.Entities.InterviewRole entity, IUrlHelper url)
        {
            var dto = new DTO.InterviewRole()
            {
        		        InterviewID = entity.InterviewID,

				        UserID = entity.UserID,

				        RoleID = entity.RoleID,

				
            };

            
            dto.Links.Add(new DTO.Link(url.Action("GetInterviewRole", "interviewroles", new { interviewid = dto.InterviewID, userid = dto.UserID  }), "self", "GET"));
            dto.Links.Add(new DTO.Link(url.Action("DeleteInterviewRole", "interviewroles", new { interviewid = dto.InterviewID, userid = dto.UserID  }), "delete_interviewrole", "DELETE"));
            dto.Links.Add(new DTO.Link(url.Action("InsertInterviewRole", "interviewroles"), "insert_interviewrole", "POST"));
            dto.Links.Add(new DTO.Link(url.Action("UpdateInterviewRole", "interviewroles"), "update_interviewrole", "PUT"));

            return dto;

        }

        public static Interfaces.Entities.InterviewRole Convert(DTO.InterviewRole dto)
        {
            var entity = new Interfaces.Entities.InterviewRole()
            {
                
        		        InterviewID = dto.InterviewID,

				        UserID = dto.UserID,

				        RoleID = dto.RoleID,

				
     
            };

            return entity;
        }
    }
}
