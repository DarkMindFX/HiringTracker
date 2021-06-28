




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class CandidatePropertyConvertor
    {
        public static DTO.CandidateProperty Convert(Interfaces.Entities.CandidateProperty entity, IUrlHelper url)
        {
            var dto = new DTO.CandidateProperty()
            {
        		        ID = entity.ID,

				        Name = entity.Name,

				        Value = entity.Value,

				        CandidateID = entity.CandidateID,

				
            };

            
            dto.Links.Add(new DTO.Link(url.Action("GetCandidateProperty", "candidateproperties", new { id = dto.ID  }), "self", "GET"));
            dto.Links.Add(new DTO.Link(url.Action("DeleteCandidateProperty", "candidateproperties", new { id = dto.ID  }), "delete_candidateproperty", "DELETE"));
            dto.Links.Add(new DTO.Link(url.Action("InsertCandidateProperty", "candidateproperties"), "insert_candidateproperty", "POST"));
            dto.Links.Add(new DTO.Link(url.Action("UpdateCandidateProperty", "candidateproperties"), "update_candidateproperty", "PUT"));

            return dto;

        }

        public static Interfaces.Entities.CandidateProperty Convert(DTO.CandidateProperty dto)
        {
            var entity = new Interfaces.Entities.CandidateProperty()
            {
                
        		        ID = dto.ID,

				        Name = dto.Name,

				        Value = dto.Value,

				        CandidateID = dto.CandidateID,

				
     
            };

            return entity;
        }
    }
}
