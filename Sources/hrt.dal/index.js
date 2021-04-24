
const CandidateSkillEntity = require('./src/entities/CandidateSkill');
const CandidateEntity = require('./src/entities/Candidate')
const InterviewRoleEntity = require('./src/entities/InterviewRole')
const InterviewStatusEntity = require('./src/entities/InterviewStatus')
const InterviewTypeEntity = require('./src/entities/InterviewType')
const PositionEntity = require('./src/entities/Position')
const PositionCandidateStatusEntity = require('./src/entities/PositionCandidateStatus')
const PositionCandidateStepEntity = require('./src/entities/PositionCandidateStep')
const PositionSkillEntity = require('./src/entities/PositionSkill')
const PositionStatusEntity = require('./src/entities/PositionStatus')
const RoleEntity = require('./src/entities/Role')
const SkillEntity = require('./src/entities/Skill')
const SkillPoficiencyEntity = require('./src/entities/SkillProficiency')
const UserEntity = require('./src/entities/User')

const InterviewRoleDal = require('./src/InterviewRoleDal')
const InterviewStatusDal = require('./src/InterviewStatusDal')
const InterviewTypeDal = require('./src/InterviewTypeDal')
const PositionCandidateStatusDal = require('./src/PositionCandidateStatusDal')
const PositionCandidateStepDal = require('./src/PositionCandidateStepDal')
const PositionDal = require('./src/PositionDal')
const PositionStatusDal = require('./src/PositionStatusDal')
const SkillPoficiencyDal = require('./src/SkillProficiencyDal')
const SkillDal = require('./src/SkillDal')
const UserDal = require('./src/UserDal')


module.exports = {
    CandidateEntity,
    CandidateSkillEntity,
    InterviewRoleEntity,
    InterviewStatusEntity,
    InterviewTypeEntity,
    PositionEntity,
    PositionCandidateStatusEntity,
    PositionCandidateStepEntity,
    PositionSkillEntity,
    PositionStatusEntity,
    RoleEntity,
    SkillEntity,
    SkillPoficiencyEntity,
    UserEntity,

    InterviewRoleDal,
    InterviewStatusDal,
    InterviewTypeDal,
    PositionCandidateStatusDal,
    PositionCandidateStepDal,
    PositionDal,
    PositionStatusDal,
    SkillPoficiencyDal,
    SkillDal,
    UserDal
}