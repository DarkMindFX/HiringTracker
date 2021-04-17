
const { Error, SkillProficiencyDto, PositionDto, UserDto, PositionStatusDto, SkillDto, PositionSkillDto } = require('hrt.dto');
const { SkillPoficiencyDal, SkillPoficiencyEntity, PositionEntity, UserEntity, PositionSkillEntity } = require('hrt.dal')

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

    static positionDto2Entity(dto) {
        let entity = new PositionEntity();
        entity.PositionID = dto._positionId;
        entity.Title = dto._title;
        entity.ShortDesc = dto._shortDesc;
        entity.Desc = dto._desc;
        entity.StatusID = dto._status._statusId;
        entity.CreatedByID = dto._createdBy ? dto._createdBy._userId : null;
        entity.CreatedDate = dto._createdDate ? dto._createdDate : null;
        entity.ModifiedByID = dto._modifiedBy ? dto._modifiedBy._userId : null;
        entity.ModifiedDate = dto._modifiedDate ? dto._modifiedDate : null;

        return entity;
    }

    static userEntity2Dto(entity) {
        let dto = new UserDto();
        dto.Login = entity.Login;
        dto.UserID = entity.UserID;
        dto.FirstName = entity.FirstName;
        dto.LastName = entity.LastName;
        dto.Description = entity.Description;
        dto.Email = entity.Email;

        return dto;
    }

    static userDto2Entity(dto) {
        let entity = new UserEntity();
        entity.Login = dto._login;
        entity.UserID = dto._userId ? parseInt(dto._userId) : null;
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

    static positionSkillDto2Entity(positionId, dto) {
        let entity = new PositionSkillEntity();
        entity.SkillID = dto._skill._skillId;
        entity.PositionID = positionId;
        entity.ProficiencyID = dto._proficiency._id;
        entity.IsMandatory = dto._isMandatory;

        return entity;
    }

}

module.exports = { Converter };