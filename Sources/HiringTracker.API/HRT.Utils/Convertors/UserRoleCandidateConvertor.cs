




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class UserRoleCandidateConvertor
    {
        public static DTO.UserRoleCandidate Convert(Interfaces.Entities.UserRoleCandidate entity, IUrlHelper url)
        {
            var dto = new DTO.UserRoleCandidate()
            {
        		        CandidateID = entity.CandidateID,

				        UserID = entity.UserID,

				        RoleID = entity.RoleID,

				
            };

            
            dto.Links.Add(new DTO.Link(url.Action("GetUserRoleCandidate", "userrolecandidates", new { candidateid = dto.CandidateID, userid = dto.UserID  }), "self", "GET"));
            dto.Links.Add(new DTO.Link(url.Action("DeleteUserRoleCandidate", "userrolecandidates", new { candidateid = dto.CandidateID, userid = dto.UserID  }), "delete_userrolecandidate", "DELETE"));
            dto.Links.Add(new DTO.Link(url.Action("InsertUserRoleCandidate", "userrolecandidates"), "insert_userrolecandidate", "POST"));
            dto.Links.Add(new DTO.Link(url.Action("UpdateUserRoleCandidate", "userrolecandidates"), "update_userrolecandidate", "PUT"));

            return dto;

        }

        public static Interfaces.Entities.UserRoleCandidate Convert(DTO.UserRoleCandidate dto)
        {
            var entity = new Interfaces.Entities.UserRoleCandidate()
            {
                
        		        CandidateID = dto.CandidateID,

				        UserID = dto.UserID,

				        RoleID = dto.RoleID,

				
     
            };

            return entity;
        }
    }
}
