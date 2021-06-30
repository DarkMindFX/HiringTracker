




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class InterviewFeedbackConvertor
    {
        public static DTO.InterviewFeedback Convert(Interfaces.Entities.InterviewFeedback entity, IUrlHelper url)
        {
            var dto = new DTO.InterviewFeedback()
            {
        		        ID = entity.ID,

				        Comment = entity.Comment,

				        Rating = entity.Rating,

				        InterviewID = entity.InterviewID,

				        InterviewerID = entity.InterviewerID,

				        CreatedByID = entity.CreatedByID,

				        CreatedDate = entity.CreatedDate,

				        ModifiedByID = entity.ModifiedByID,

				        ModifiedDate = entity.ModifiedDate,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetInterviewFeedback", "interviewfeedbacks", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteInterviewFeedback", "interviewfeedbacks", new { id = dto.ID  }), "delete_interviewfeedback", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertInterviewFeedback", "interviewfeedbacks"), "insert_interviewfeedback", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateInterviewFeedback", "interviewfeedbacks"), "update_interviewfeedback", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.InterviewFeedback Convert(DTO.InterviewFeedback dto)
        {
            var entity = new Interfaces.Entities.InterviewFeedback()
            {
                
        		        ID = dto.ID,

				        Comment = dto.Comment,

				        Rating = dto.Rating,

				        InterviewID = dto.InterviewID,

				        InterviewerID = dto.InterviewerID,

				        CreatedByID = dto.CreatedByID,

				        CreatedDate = dto.CreatedDate,

				        ModifiedByID = dto.ModifiedByID,

				        ModifiedDate = dto.ModifiedDate,

				
     
            };

            return entity;
        }
    }
}
