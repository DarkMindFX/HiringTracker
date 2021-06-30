




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class ProposalConvertor
    {
        public static DTO.Proposal Convert(Interfaces.Entities.Proposal entity, IUrlHelper url)
        {
            var dto = new DTO.Proposal()
            {
        		        ID = entity.ID,

				        PositionID = entity.PositionID,

				        CandidateID = entity.CandidateID,

				        Proposed = entity.Proposed,

				        CurrentStepID = entity.CurrentStepID,

				        StepSetDate = entity.StepSetDate,

				        NextStepID = entity.NextStepID,

				        DueDate = entity.DueDate,

				        StatusID = entity.StatusID,

				        CreatedByID = entity.CreatedByID,

				        CreatedDate = entity.CreatedDate,

				        ModifiedByID = entity.ModifiedByID,

				        ModifiedDate = entity.ModifiedDate,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetProposal", "proposals", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteProposal", "proposals", new { id = dto.ID  }), "delete_proposal", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertProposal", "proposals"), "insert_proposal", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateProposal", "proposals"), "update_proposal", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.Proposal Convert(DTO.Proposal dto)
        {
            var entity = new Interfaces.Entities.Proposal()
            {
                
        		        ID = dto.ID,

				        PositionID = dto.PositionID,

				        CandidateID = dto.CandidateID,

				        Proposed = dto.Proposed,

				        CurrentStepID = dto.CurrentStepID,

				        StepSetDate = dto.StepSetDate,

				        NextStepID = dto.NextStepID,

				        DueDate = dto.DueDate,

				        StatusID = dto.StatusID,

				        CreatedByID = dto.CreatedByID,

				        CreatedDate = dto.CreatedDate,

				        ModifiedByID = dto.ModifiedByID,

				        ModifiedDate = dto.ModifiedDate,

				
     
            };

            return entity;
        }
    }
}
