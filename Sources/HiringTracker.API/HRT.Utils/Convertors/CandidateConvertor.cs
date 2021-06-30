




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class CandidateConvertor
    {
        public static DTO.Candidate Convert(Interfaces.Entities.Candidate entity, IUrlHelper url)
        {
            var dto = new DTO.Candidate()
            {
        		        ID = entity.ID,

				        FirstName = entity.FirstName,

				        MiddleName = entity.MiddleName,

				        LastName = entity.LastName,

				        Email = entity.Email,

				        Phone = entity.Phone,

				        CVLink = entity.CVLink,

				        CreatedByID = entity.CreatedByID,

				        CreatedDate = entity.CreatedDate,

				        ModifiedByID = entity.ModifiedByID,

				        ModifiedDate = entity.ModifiedDate,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetCandidate", "candidates", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteCandidate", "candidates", new { id = dto.ID  }), "delete_candidate", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertCandidate", "candidates"), "insert_candidate", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateCandidate", "candidates"), "update_candidate", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.Candidate Convert(DTO.Candidate dto)
        {
            var entity = new Interfaces.Entities.Candidate()
            {
                
        		        ID = dto.ID,

				        FirstName = dto.FirstName,

				        MiddleName = dto.MiddleName,

				        LastName = dto.LastName,

				        Email = dto.Email,

				        Phone = dto.Phone,

				        CVLink = dto.CVLink,

				        CreatedByID = dto.CreatedByID,

				        CreatedDate = dto.CreatedDate,

				        ModifiedByID = dto.ModifiedByID,

				        ModifiedDate = dto.ModifiedDate,

				
     
            };

            return entity;
        }
    }
}
