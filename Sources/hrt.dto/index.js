
const Error = require('./src/Error')
const HateosDto = require('./src/HateosDto')
const SkillDto = require('./src/SkillDto')
const SkillProficiencyDto = require('./src/SkillProficiencyDto')
const HealthResponse = require('./src/Health')
const { LoginRequest, LoginResponse } = require('./src/Login')
const UserDto = require('./src/UserDto')
const PositionDto = require('./src/PositionDto')
const { CandidateUpsertDto, CandidateUpsertResponseDto } = require('./src/CandidateUpsertDto')
const PositionStatusDto = require('./src/PositionStatusDto')
const PositionSkillDto = require('./src/PositionSkillDto')
const CandidateDto = require('./src/CandidateDto')
const CandidateSkillDto = require('./src/CandidateSkillDto')
const PositionCandidateStatusDto = require('./src/PositionCandidateStatusDto')
const PositionCandidateStepDto = require('./src/PositionCandidateStepDto')


module.exports = {
    Error,
    HealthResponse,
    LoginRequest,
    LoginResponse,
    HateosDto,
    SkillDto,
    SkillProficiencyDto,
    UserDto,
    PositionDto,
    PositionStatusDto,
    PositionSkillDto,
    CandidateDto,
    CandidateSkillDto,
    CandidateUpsertDto,
    CandidateUpsertResponseDto,
    PositionCandidateStatusDto,
    PositionCandidateStepDto
}