using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRT.HiringTracker.API.Helpers
{
    public static class EntityToDtoConvertor
    {
        #region Entity-2-DTO

        public static DTO.User Convert(Interfaces.Entities.User entity, IUrlHelper url)
        {
            var dto = new DTO.User()
            {
                Description = entity.Description,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Login = entity.Login,
                UserID = entity.UserID
            };

            dto.Links.Add(new DTO.Link(url.Action("GetUser", "users", new { id = dto.UserID }), "self", "GET"));
            dto.Links.Add(new DTO.Link(url.Action("DeleteUser", "users", new { id = dto.UserID }), "delete_user", "DELETE"));
            dto.Links.Add(new DTO.Link(url.Action("UpdateUser", "users"), "update_user", "PUT"));

            return dto;
        }

        public static DTO.PositionStatus Convert(Interfaces.Entities.PositionStatus entity, IUrlHelper url)
        {
            var dto = new DTO.PositionStatus()
            {
                Name = entity.Name,
                StatusID = entity.StatusID
            };

            return dto;
        }

        public static DTO.PositionCandidateStatus Convert(Interfaces.Entities.PositionCandidateStatus entity, IUrlHelper url)
        {
            var dto = new DTO.PositionCandidateStatus()
            {
                Name = entity.Name,
                StatusID = entity.StatusID
            };

            return dto;
        }

        public static DTO.PositionCandidateStep Convert(Interfaces.Entities.PositionCandidateStep entity, IUrlHelper url)
        {
            var dto = new DTO.PositionCandidateStep()
            {
                Name = entity.Name,
                StepID = entity.StepID,
                ReqDueDate = entity.ReqDueDate,
                RequiresRespInDays = entity.RequiresRespInDays
            };

            return dto;
        }

        public static DTO.Skill Convert(Interfaces.Entities.Skill entity, IUrlHelper url)
        {
            var dto = new DTO.Skill()
            {
                Name = entity.Name,
                SkillID = entity.SkillID
            };

            dto.Links.Add(new DTO.Link(url.Action("DeleteSkill", "skills", new { id = entity.SkillID }), "delete_skill", "DELETE"));
            dto.Links.Add(new DTO.Link(url.Action("UpdateSkill", "skills"), "update_skill", "PUT"));

            return dto;
        }

        public static DTO.SkillProficiency Convert(Interfaces.Entities.SkillProficiency entity, IUrlHelper url)
        {
            var dto = new DTO.SkillProficiency()
            {
                Name = entity.Name,
                ProficiencyID = entity.ProficiencyID
            };

            dto.Links.Add(new DTO.Link(url.Action("DeleteSkillProficiency", "skillproficiencies", new { id = entity.ProficiencyID }), "delete_proficiency", "DELETE"));
            dto.Links.Add(new DTO.Link(url.Action("UpdateSkillProficiency", "skillproficiencies"), "update_proficiency", "PUT"));

            return dto;
        }

        #endregion

        #region DTO-2-Entity
        public static Interfaces.Entities.Skill Convert(DTO.Skill dto)
        {
            var entity = new Interfaces.Entities.Skill()
            {
                Name = dto.Name,
                SkillID = dto.SkillID
            };

            return entity;
        }

        public static Interfaces.Entities.User Convert(DTO.User dto)
        {
            var entity = new Interfaces.Entities.User()
            {
                Description = dto.Description,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Login = dto.Login,
                UserID = dto.UserID
            };

            return entity;
        }

        #endregion
    }
}
