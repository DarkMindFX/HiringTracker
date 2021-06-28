




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class ProposalStepConvertor
    {
        public static DTO.ProposalStep Convert(Interfaces.Entities.ProposalStep entity, IUrlHelper url)
        {
            var dto = new DTO.ProposalStep()
            {
        		        ID = entity.ID,

				        Name = entity.Name,

				        ReqDueDate = entity.ReqDueDate,

				        RequiresRespInDays = entity.RequiresRespInDays,

				
            };

            
            dto.Links.Add(new DTO.Link(url.Action("GetProposalStep", "proposalsteps", new { id = dto.ID  }), "self", "GET"));
            dto.Links.Add(new DTO.Link(url.Action("DeleteProposalStep", "proposalsteps", new { id = dto.ID  }), "delete_proposalstep", "DELETE"));
            dto.Links.Add(new DTO.Link(url.Action("InsertProposalStep", "proposalsteps"), "insert_proposalstep", "POST"));
            dto.Links.Add(new DTO.Link(url.Action("UpdateProposalStep", "proposalsteps"), "update_proposalstep", "PUT"));

            return dto;

        }

        public static Interfaces.Entities.ProposalStep Convert(DTO.ProposalStep dto)
        {
            var entity = new Interfaces.Entities.ProposalStep()
            {
                
        		        ID = dto.ID,

				        Name = dto.Name,

				        ReqDueDate = dto.ReqDueDate,

				        RequiresRespInDays = dto.RequiresRespInDays,

				
     
            };

            return entity;
        }
    }
}
