




using Microsoft.AspNetCore.Mvc;
using System;

namespace HRT.Utils.Convertors
{
    public class SkillConvertor
    {
        public static DTO.Skill Convert(Interfaces.Entities.Skill entity, IUrlHelper url)
        {
            var dto = new DTO.Skill()
            {
        		        ID = entity.ID,

				        Name = entity.Name,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetSkill", "skills", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteSkill", "skills", new { id = dto.ID  }), "delete_skill", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertSkill", "skills"), "insert_skill", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateSkill", "skills"), "update_skill", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.Skill Convert(DTO.Skill dto)
        {
            var entity = new Interfaces.Entities.Skill()
            {
                
        		        ID = dto.ID,

				        Name = dto.Name,

				
     
            };

            return entity;
        }
    }
}
