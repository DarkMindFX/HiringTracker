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

        public static DTO.PositionSkill Convert(Interfaces.Entities.PositionSkill entity,
                                        IDictionary<long, Interfaces.Entities.Skill> skills,
                                        IDictionary<long, Interfaces.Entities.SkillProficiency> profs,
                                        IUrlHelper url)
        {
            var dto = new DTO.PositionSkill()
            {
                IsMandatory = entity.IsMandatory,
                Proficiency = Convert(profs[entity.ProficiencyID], url),
                Skill = Convert(skills[entity.SkillID], url)
            };

            return dto;
        }

        public static DTO.CandidateSkill Convert(Interfaces.Entities.CandidateSkill entity,
                                        IDictionary<long, Interfaces.Entities.Skill> skills,
                                        IDictionary<long, Interfaces.Entities.SkillProficiency> profs,
                                        IUrlHelper url)
        {
            var dto = new DTO.CandidateSkill()
            {
                Proficiency = Convert(profs[entity.ProficiencyID], url),
                Skill = Convert(skills[entity.SkillID], url)
            };

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

        public static DTO.Position Convert(Interfaces.Entities.Position entity,
                                            IDictionary<long, Interfaces.Entities.PositionStatus> statuses,
                                            IDictionary<long, Interfaces.Entities.User> users,
                                            IUrlHelper url)
        {
            var dto = new DTO.Position()
            {
                CreatedBy = Convert(users[entity.CreatedByID], url),
                CreatedDate = entity.CreatedDate,
                Description = entity.Description,
                ModifiedBy = entity.ModifiedByID != null ? Convert(users[(long)entity.ModifiedByID], url) : null,
                ModifiedDate = entity.ModifiedDate,
                PositionID = entity.PositionID ?? 0,
                ShortDesc = entity.ShortDesc,
                Status = Convert(statuses[entity.StatusID], url),
                Title = entity.Title
            };

            dto.Links.Add(new DTO.Link(url.Action("GetPosition", "positions", new { id = dto.PositionID }), "self", "GET"));
            dto.Links.Add(new DTO.Link(url.Action("DeletePosition", "positions", new { id = dto.PositionID }), "delete_position", "DELETE"));
            dto.Links.Add(new DTO.Link(url.Action("UpdatePosition", "positions"), "update_position", "PUT"));

            return dto;

        }

        public static DTO.Candidate Convert(Interfaces.Entities.Candidate entity,
                                            IDictionary<long, Interfaces.Entities.User> users,
                                            IUrlHelper url)
        {
            var dto = new DTO.Candidate()
            {
                CreatedBy = Convert(users[entity.CreatedByID], url),
                CreatedDate = entity.CreatedDate,
                ModifiedBy = entity.ModifiedByID != null ? Convert(users[(long)entity.ModifiedByID], url) : null,
                ModifiedDate = entity.ModifiedDate,
                CandidateID = entity.CandidateID ?? 0,
                CVLink = entity.CVLink,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                MiddleName = entity.MiddleName,
                Phone = entity.Phone                
            };

            dto.Links.Add(new DTO.Link(url.Action("GetCandidate", "candidates", new { id = dto.CandidateID }), "self", "GET"));
            dto.Links.Add(new DTO.Link(url.Action("DeleteCandidate", "candidates", new { id = dto.CandidateID }), "delete_candidate", "DELETE"));
            dto.Links.Add(new DTO.Link(url.Action("UpdateCandidate", "candidates"), "update_candidate", "PUT"));

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

        public static Interfaces.Entities.Position Convert(DTO.Position dto)
        {
            var entity = new Interfaces.Entities.Position()
            {
                Description = dto.Description,
                DepartmentID = dto.DepartmentID,
                PositionID = dto.PositionID,
                ShortDesc = dto.ShortDesc,
                StatusID = dto.Status.StatusID,
                Title = dto.Title
            };

            return entity;
        }

        public static Interfaces.Entities.Candidate Convert(DTO.Candidate dto)
        {
            var entity = new Interfaces.Entities.Candidate()
            {
                CandidateID = dto.CandidateID,
                CVLink = dto.CVLink,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Phone = dto.Phone,
                MiddleName = dto.MiddleName
            };

            return entity;
        }

        public static Interfaces.Entities.PositionSkill Convert(DTO.PositionSkill dto)
        {
            var entity = new Interfaces.Entities.PositionSkill()
            {
                IsMandatory = dto.IsMandatory,
                SkillID = dto.Skill.SkillID,
                ProficiencyID = dto.Proficiency.ProficiencyID
            };

            return entity;
        }

        public static Interfaces.Entities.CandidateSkill Convert(DTO.CandidateSkill dto)
        {
            var entity = new Interfaces.Entities.CandidateSkill()
            {
                SkillID = dto.Skill.SkillID,
                ProficiencyID = dto.Proficiency.ProficiencyID
            };

            return entity;
        }

        #endregion
    }
}
