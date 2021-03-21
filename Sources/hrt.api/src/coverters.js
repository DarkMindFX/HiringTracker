
const { Error, SkillProficiencyDto, PositionDto, UserDto, PositionStatusDto } = require('hrt.dto');
const { SkillPoficiencyDal, SkillPoficiencyEntity, PositionEntity, UserEntity } = require('hrt.dal')


function skillProficiencyEntity2Dto(entity) {
    let dto = new SkillProficiencyDto();
    dto.ProficiencyID = entity.ProficiencyID;
    dto.Name = entity.Name;

    return dto;
}

function positionEntity2Dto(entity, dictUsers, dictStatuses) {
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

function userEntity2Dto(entity) {
    let dto = new UserDto();
    dto.UserID = entity.UserID;
    dto.FirstName = entity.FirstName;
    dto.LastName = entity.LastName;
    dto.Description = entity.Description;
    dto.Email = entity.Email;

    return dto;
}

function positionStatusEntity2Dto(entity) {
    let dto = new PositionStatusDto();
    dto.StatusID = entity.StatusID;
    dto.Name = entity.Name;

    return dto;
}

module.exports = {
    skillProficiencyEntity2Dto,
    positionEntity2Dto,
    userEntity2Dto,
    positionStatusEntity2Dto
}