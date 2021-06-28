




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class PositionSkillConvertor
    {
        public static DTO.PositionSkill Convert(Interfaces.Entities.PositionSkill entity, IUrlHelper url)
        {
            var dto = new DTO.PositionSkill()
            {
        		        PositionID = entity.PositionID,

				        SkillID = entity.SkillID,

				        IsMandatory = entity.IsMandatory,

				        SkillProficiencyID = entity.SkillProficiencyID,

				
            };

            
            dto.Links.Add(new DTO.Link(url.Action("GetPositionSkill", "positionskills", new { positionid = dto.PositionID, skillid = dto.SkillID  }), "self", "GET"));
            dto.Links.Add(new DTO.Link(url.Action("DeletePositionSkill", "positionskills", new { positionid = dto.PositionID, skillid = dto.SkillID  }), "delete_positionskill", "DELETE"));
            dto.Links.Add(new DTO.Link(url.Action("InsertPositionSkill", "positionskills"), "insert_positionskill", "POST"));
            dto.Links.Add(new DTO.Link(url.Action("UpdatePositionSkill", "positionskills"), "update_positionskill", "PUT"));

            return dto;

        }

        public static Interfaces.Entities.PositionSkill Convert(DTO.PositionSkill dto)
        {
            var entity = new Interfaces.Entities.PositionSkill()
            {
                
        		        PositionID = dto.PositionID,

				        SkillID = dto.SkillID,

				        IsMandatory = dto.IsMandatory,

				        SkillProficiencyID = dto.SkillProficiencyID,

				
     
            };

            return entity;
        }
    }
}
