
const { Error, SkillProficiencyDto, PositionDto, UserDto, PositionStatusDto, SkillDto, PositionSkillDto } = require('hrt.dto');
const { SkillPoficiencyDal, SkillPoficiencyEntity, PositionEntity, UserEntity } = require('hrt.dal')

class Converter {

    static skillProficiencyEntity2Dto(entity) {
        let dto = new SkillProficiencyDto();
        dto.ProficiencyID = entity.ProficiencyID;
        dto.Name = entity.Name;

        return dto;
    }

    static positionEntity2Dto(entity, dictUsers, dictStatuses) {
        let dto = new PositionDto();
        dto.PositionID = entity.PositionID;
        dto.Title = entity.Title;
        dto.ShortDesc = entity.ShortDesc;
        dto.Desc = entity.Desc;
        dto.Status = dictStatuses[entity.StatusID];
        dto.CreatedBy = dictUsers[entity.CreatedByID]
        dto.CreatedDate = entity.CreatedDate;
        dto.ModifiedBy = entity.ModifiedByID ? dictUsers[entity.ModifiedByID] : null
        dto.ModifiedDate = entity.ModifiedDate;

        return dto;
    }

    static userEntity2Dto(entity) {
        let dto = new UserDto();
        dto.UserID = entity.UserID;
        dto.FirstName = entity.FirstName;
        dto.LastName = entity.LastName;
        dto.Description = entity.Description;
        dto.Email = entity.Email;

        return dto;
    }

    static userDto2Entity(dto) {
        let entity = new UserEntity();
        entity.UserID = dto._userId;
        entity.FirstName = dto._fname;
        entity.LastName = dto._lname;
        entity.Description = dto._desc;
        entity.Email = dto._email;

        return entity;
    }

    static positionStatusEntity2Dto(entity) {
        let dto = new PositionStatusDto();
        dto.StatusID = entity.StatusID;
        dto.Name = entity.Name;

        return dto;
    }

    static skillEntity2Dto(entity) {
        let dto = new SkillDto();
        dto.SkillID = entity.SkillID;
        dto.Name = entity.Name;   

        return dto;
    }

    static positionSkillEntity2Dto(entity, dictSkills, dictProficiencies) {
        let dto = new PositionSkillDto();
        dto.Skill = dictSkills[entity.SkillID];
        dto.Proficiency = dictProficiencies[entity.ProficiencyID]; 
        dto.IsMandatory = entity.IsMandatory;  

        return dto;
    }

}

module.exports = { Converter };