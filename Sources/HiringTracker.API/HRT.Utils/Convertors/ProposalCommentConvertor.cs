




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class ProposalCommentConvertor
    {
        public static DTO.ProposalComment Convert(Interfaces.Entities.ProposalComment entity, IUrlHelper url)
        {
            var dto = new DTO.ProposalComment()
            {
        		        ProposalID = entity.ProposalID,

				        CommentID = entity.CommentID,

				
            };

            
            dto.Links.Add(new DTO.Link(url.Action("GetProposalComment", "proposalcomments", new { proposalid = dto.ProposalID, commentid = dto.CommentID  }), "self", "GET"));
            dto.Links.Add(new DTO.Link(url.Action("DeleteProposalComment", "proposalcomments", new { proposalid = dto.ProposalID, commentid = dto.CommentID  }), "delete_proposalcomment", "DELETE"));
            dto.Links.Add(new DTO.Link(url.Action("InsertProposalComment", "proposalcomments"), "insert_proposalcomment", "POST"));
            dto.Links.Add(new DTO.Link(url.Action("UpdateProposalComment", "proposalcomments"), "update_proposalcomment", "PUT"));

            return dto;

        }

        public static Interfaces.Entities.ProposalComment Convert(DTO.ProposalComment dto)
        {
            var entity = new Interfaces.Entities.ProposalComment()
            {
                
        		        ProposalID = dto.ProposalID,

				        CommentID = dto.CommentID,

				
     
            };

            return entity;
        }
    }
}
