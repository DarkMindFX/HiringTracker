




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class CommentConvertor
    {
        public static DTO.Comment Convert(Interfaces.Entities.Comment entity, IUrlHelper url)
        {
            var dto = new DTO.Comment()
            {
        		        ID = entity.ID,

				        Text = entity.Text,

				        CreatedDate = entity.CreatedDate,

				        CreatedByID = entity.CreatedByID,

				        ModifiedDate = entity.ModifiedDate,

				        ModifiedByID = entity.ModifiedByID,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetComment", "comments", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteComment", "comments", new { id = dto.ID  }), "delete_comment", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertComment", "comments"), "insert_comment", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateComment", "comments"), "update_comment", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.Comment Convert(DTO.Comment dto)
        {
            var entity = new Interfaces.Entities.Comment()
            {
                
        		        ID = dto.ID,

				        Text = dto.Text,

				        CreatedDate = dto.CreatedDate,

				        CreatedByID = dto.CreatedByID,

				        ModifiedDate = dto.ModifiedDate,

				        ModifiedByID = dto.ModifiedByID,

				
     
            };

            return entity;
        }
    }
}
