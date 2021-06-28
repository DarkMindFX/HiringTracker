




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class RoleConvertor
    {
        public static DTO.Role Convert(Interfaces.Entities.Role entity, IUrlHelper url)
        {
            var dto = new DTO.Role()
            {
        		        ID = entity.ID,

				        Name = entity.Name,

				
            };

            
            dto.Links.Add(new DTO.Link(url.Action("GetRole", "roles", new { id = dto.ID  }), "self", "GET"));
            dto.Links.Add(new DTO.Link(url.Action("DeleteRole", "roles", new { id = dto.ID  }), "delete_role", "DELETE"));
            dto.Links.Add(new DTO.Link(url.Action("InsertRole", "roles"), "insert_role", "POST"));
            dto.Links.Add(new DTO.Link(url.Action("UpdateRole", "roles"), "update_role", "PUT"));

            return dto;

        }

        public static Interfaces.Entities.Role Convert(DTO.Role dto)
        {
            var entity = new Interfaces.Entities.Role()
            {
                
        		        ID = dto.ID,

				        Name = dto.Name,

				
     
            };

            return entity;
        }
    }
}
