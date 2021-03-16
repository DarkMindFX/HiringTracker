
const { Error, SkillProficiencyDto } = require('hrt.dto');
const { SkillPoficiencyDal, SkillPoficiencyEntity } = require('hrt.dal')

function skillProficiencyEntity2Dto(entity) {
    let dto = new SkillProficiencyDto();
    dto.ProficiencyID = entity.ProficiencyID;
    dto.Name = entity.Name;

    return dto;
}

mdoule.exports = {
    skillProficiencyEntity2Dto
}