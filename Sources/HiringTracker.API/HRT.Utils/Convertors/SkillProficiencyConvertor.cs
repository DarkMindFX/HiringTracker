




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class SkillProficiencyConvertor
    {
        public static DTO.SkillProficiency Convert(Interfaces.Entities.SkillProficiency entity, IUrlHelper url)
        {
            var dto = new DTO.SkillProficiency()
            {
        		        ID = entity.ID,

				        Name = entity.Name,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetSkillProficiency", "skillproficiencies", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteSkillProficiency", "skillproficiencies", new { id = dto.ID  }), "delete_skillproficiency", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertSkillProficiency", "skillproficiencies"), "insert_skillproficiency", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateSkillProficiency", "skillproficiencies"), "update_skillproficiency", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.SkillProficiency Convert(DTO.SkillProficiency dto)
        {
            var entity = new Interfaces.Entities.SkillProficiency()
            {
                
        		        ID = dto.ID,

				        Name = dto.Name,

				
     
            };

            return entity;
        }
    }
}
