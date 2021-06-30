




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class PositionCommentConvertor
    {
        public static DTO.PositionComment Convert(Interfaces.Entities.PositionComment entity, IUrlHelper url)
        {
            var dto = new DTO.PositionComment()
            {
        		        PositionID = entity.PositionID,

				        CommentID = entity.CommentID,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetPositionComment", "positioncomments", new { positionid = dto.PositionID, commentid = dto.CommentID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeletePositionComment", "positioncomments", new { positionid = dto.PositionID, commentid = dto.CommentID  }), "delete_positioncomment", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertPositionComment", "positioncomments"), "insert_positioncomment", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdatePositionComment", "positioncomments"), "update_positioncomment", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.PositionComment Convert(DTO.PositionComment dto)
        {
            var entity = new Interfaces.Entities.PositionComment()
            {
                
        		        PositionID = dto.PositionID,

				        CommentID = dto.CommentID,

				
     
            };

            return entity;
        }
    }
}
