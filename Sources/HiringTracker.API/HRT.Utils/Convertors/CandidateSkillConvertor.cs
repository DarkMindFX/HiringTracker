




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class CandidateSkillConvertor
    {
        public static DTO.CandidateSkill Convert(Interfaces.Entities.CandidateSkill entity, IUrlHelper url)
        {
            var dto = new DTO.CandidateSkill()
            {
        		        CandidateID = entity.CandidateID,

				        SkillID = entity.SkillID,

				        SkillProficiencyID = entity.SkillProficiencyID,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetCandidateSkill", "candidateskills", new { candidateid = dto.CandidateID, skillid = dto.SkillID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteCandidateSkill", "candidateskills", new { candidateid = dto.CandidateID, skillid = dto.SkillID  }), "delete_candidateskill", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertCandidateSkill", "candidateskills"), "insert_candidateskill", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateCandidateSkill", "candidateskills"), "update_candidateskill", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.CandidateSkill Convert(DTO.CandidateSkill dto)
        {
            var entity = new Interfaces.Entities.CandidateSkill()
            {
                
        		        CandidateID = dto.CandidateID,

				        SkillID = dto.SkillID,

				        SkillProficiencyID = dto.SkillProficiencyID,

				
     
            };

            return entity;
        }
    }
}
