




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class UserRoleSystemConvertor
    {
        public static DTO.UserRoleSystem Convert(Interfaces.Entities.UserRoleSystem entity, IUrlHelper url)
        {
            var dto = new DTO.UserRoleSystem()
            {
        		        UserID = entity.UserID,

				        RoleID = entity.RoleID,

				
            };

            
            dto.Links.Add(new DTO.Link(url.Action("GetUserRoleSystem", "userrolesystems", new { userid = dto.UserID, roleid = dto.RoleID  }), "self", "GET"));
            dto.Links.Add(new DTO.Link(url.Action("DeleteUserRoleSystem", "userrolesystems", new { userid = dto.UserID, roleid = dto.RoleID  }), "delete_userrolesystem", "DELETE"));
            dto.Links.Add(new DTO.Link(url.Action("InsertUserRoleSystem", "userrolesystems"), "insert_userrolesystem", "POST"));
            dto.Links.Add(new DTO.Link(url.Action("UpdateUserRoleSystem", "userrolesystems"), "update_userrolesystem", "PUT"));

            return dto;

        }

        public static Interfaces.Entities.UserRoleSystem Convert(DTO.UserRoleSystem dto)
        {
            var entity = new Interfaces.Entities.UserRoleSystem()
            {
                
        		        UserID = dto.UserID,

				        RoleID = dto.RoleID,

				
     
            };

            return entity;
        }
    }
}
