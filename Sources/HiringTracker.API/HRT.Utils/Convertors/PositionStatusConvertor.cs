




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class PositionStatusConvertor
    {
        public static DTO.PositionStatus Convert(Interfaces.Entities.PositionStatus entity, IUrlHelper url)
        {
            var dto = new DTO.PositionStatus()
            {
        		        ID = entity.ID,

				        Name = entity.Name,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetPositionStatus", "positionstatuss", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeletePositionStatus", "positionstatuss", new { id = dto.ID  }), "delete_positionstatus", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertPositionStatus", "positionstatuss"), "insert_positionstatus", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdatePositionStatus", "positionstatuss"), "update_positionstatus", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.PositionStatus Convert(DTO.PositionStatus dto)
        {
            var entity = new Interfaces.Entities.PositionStatus()
            {
                
        		        ID = dto.ID,

				        Name = dto.Name,

				
     
            };

            return entity;
        }
    }
}
