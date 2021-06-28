




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class UserRolePositionConvertor
    {
        public static DTO.UserRolePosition Convert(Interfaces.Entities.UserRolePosition entity, IUrlHelper url)
        {
            var dto = new DTO.UserRolePosition()
            {
        		        PositionID = entity.PositionID,

				        UserID = entity.UserID,

				        RoleID = entity.RoleID,

				
            };

            
            dto.Links.Add(new DTO.Link(url.Action("GetUserRolePosition", "userrolepositions", new { positionid = dto.PositionID, userid = dto.UserID  }), "self", "GET"));
            dto.Links.Add(new DTO.Link(url.Action("DeleteUserRolePosition", "userrolepositions", new { positionid = dto.PositionID, userid = dto.UserID  }), "delete_userroleposition", "DELETE"));
            dto.Links.Add(new DTO.Link(url.Action("InsertUserRolePosition", "userrolepositions"), "insert_userroleposition", "POST"));
            dto.Links.Add(new DTO.Link(url.Action("UpdateUserRolePosition", "userrolepositions"), "update_userroleposition", "PUT"));

            return dto;

        }

        public static Interfaces.Entities.UserRolePosition Convert(DTO.UserRolePosition dto)
        {
            var entity = new Interfaces.Entities.UserRolePosition()
            {
                
        		        PositionID = dto.PositionID,

				        UserID = dto.UserID,

				        RoleID = dto.RoleID,

				
     
            };

            return entity;
        }
    }
}
