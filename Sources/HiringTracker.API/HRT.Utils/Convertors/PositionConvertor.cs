




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class PositionConvertor
    {
        public static DTO.Position Convert(Interfaces.Entities.Position entity, IUrlHelper url)
        {
            var dto = new DTO.Position()
            {
        		        ID = entity.ID,

				        DepartmentID = entity.DepartmentID,

				        Title = entity.Title,

				        ShortDesc = entity.ShortDesc,

				        Description = entity.Description,

				        StatusID = entity.StatusID,

				        CreatedDate = entity.CreatedDate,

				        CreatedByID = entity.CreatedByID,

				        ModifiedDate = entity.ModifiedDate,

				        ModifiedByID = entity.ModifiedByID,

				
            };

            
            dto.Links.Add(new DTO.Link(url.Action("GetPosition", "positions", new { id = dto.ID  }), "self", "GET"));
            dto.Links.Add(new DTO.Link(url.Action("DeletePosition", "positions", new { id = dto.ID  }), "delete_position", "DELETE"));
            dto.Links.Add(new DTO.Link(url.Action("InsertPosition", "positions"), "insert_position", "POST"));
            dto.Links.Add(new DTO.Link(url.Action("UpdatePosition", "positions"), "update_position", "PUT"));

            return dto;

        }

        public static Interfaces.Entities.Position Convert(DTO.Position dto)
        {
            var entity = new Interfaces.Entities.Position()
            {
                
        		        ID = dto.ID,

				        DepartmentID = dto.DepartmentID,

				        Title = dto.Title,

				        ShortDesc = dto.ShortDesc,

				        Description = dto.Description,

				        StatusID = dto.StatusID,

				        CreatedDate = dto.CreatedDate,

				        CreatedByID = dto.CreatedByID,

				        ModifiedDate = dto.ModifiedDate,

				        ModifiedByID = dto.ModifiedByID,

				
     
            };

            return entity;
        }
    }
}
