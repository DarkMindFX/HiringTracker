




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class ProposalStatusConvertor
    {
        public static DTO.ProposalStatus Convert(Interfaces.Entities.ProposalStatus entity, IUrlHelper url)
        {
            var dto = new DTO.ProposalStatus()
            {
        		        ID = entity.ID,

				        Name = entity.Name,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetProposalStatus", "proposalstatuses", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteProposalStatus", "proposalstatuses", new { id = dto.ID  }), "delete_proposalstatus", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertProposalStatus", "proposalstatuses"), "insert_proposalstatus", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateProposalStatus", "proposalstatuses"), "update_proposalstatus", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.ProposalStatus Convert(DTO.ProposalStatus dto)
        {
            var entity = new Interfaces.Entities.ProposalStatus()
            {
                
        		        ID = dto.ID,

				        Name = dto.Name,

				
     
            };

            return entity;
        }
    }
}
