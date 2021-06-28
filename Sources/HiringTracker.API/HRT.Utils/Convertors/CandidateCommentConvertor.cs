




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class CandidateCommentConvertor
    {
        public static DTO.CandidateComment Convert(Interfaces.Entities.CandidateComment entity, IUrlHelper url)
        {
            var dto = new DTO.CandidateComment()
            {
        		        CandidateID = entity.CandidateID,

				        CommentID = entity.CommentID,

				
            };

            
            dto.Links.Add(new DTO.Link(url.Action("GetCandidateComment", "candidatecomments", new { candidateid = dto.CandidateID, commentid = dto.CommentID  }), "self", "GET"));
            dto.Links.Add(new DTO.Link(url.Action("DeleteCandidateComment", "candidatecomments", new { candidateid = dto.CandidateID, commentid = dto.CommentID  }), "delete_candidatecomment", "DELETE"));
            dto.Links.Add(new DTO.Link(url.Action("InsertCandidateComment", "candidatecomments"), "insert_candidatecomment", "POST"));
            dto.Links.Add(new DTO.Link(url.Action("UpdateCandidateComment", "candidatecomments"), "update_candidatecomment", "PUT"));

            return dto;

        }

        public static Interfaces.Entities.CandidateComment Convert(DTO.CandidateComment dto)
        {
            var entity = new Interfaces.Entities.CandidateComment()
            {
                
        		        CandidateID = dto.CandidateID,

				        CommentID = dto.CommentID,

				
     
            };

            return entity;
        }
    }
}
