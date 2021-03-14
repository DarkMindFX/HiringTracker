
const SkillPoficiencyEntity = require('./src/entities/SkillProficiency')
const SkillEntity = require('./src/entities/Skill')
const PositionStatusEntity = require('./src/entities/PositionStatus')
const RoleEntity = require('./src/entities/Role')
const UserEntity = require('./src/entities/User')

const SkillPoficiencyDal = require('./src/SkillProficiencyDal')
const SkillDal = require('./src/SkillDal')

module.exports = {
    SkillPoficiencyEntity,
    SkillEntity,
    PositionStatusEntity,
    RoleEntity,
    UserEntity,

    SkillPoficiencyDal,
    SkillDal

}