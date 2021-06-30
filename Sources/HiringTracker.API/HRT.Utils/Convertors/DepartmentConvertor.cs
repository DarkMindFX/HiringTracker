




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class DepartmentConvertor
    {
        public static DTO.Department Convert(Interfaces.Entities.Department entity, IUrlHelper url)
        {
            var dto = new DTO.Department()
            {
        		        ID = entity.ID,

				        Name = entity.Name,

				        UUID = entity.UUID,

				        ParentID = entity.ParentID,

				        ManagerID = entity.ManagerID,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetDepartment", "departments", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteDepartment", "departments", new { id = dto.ID  }), "delete_department", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertDepartment", "departments"), "insert_department", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateDepartment", "departments"), "update_department", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.Department Convert(DTO.Department dto)
        {
            var entity = new Interfaces.Entities.Department()
            {
                
        		        ID = dto.ID,

				        Name = dto.Name,

				        UUID = dto.UUID,

				        ParentID = dto.ParentID,

				        ManagerID = dto.ManagerID,

				
     
            };

            return entity;
        }
    }
}
