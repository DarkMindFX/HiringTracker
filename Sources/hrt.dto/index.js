
const Error = require('./src/Error')
const HateosDto = require('./src/HateosDto')
const SkillDto = require('./src/SkillDto')
const SkillProficiencyDto = require('./src/SkillProficiencyDto')
const HealthResponse = require('./src/Health')
const { LoginRequest, LoginResponse } = require('./src/Login')
const UserDto = require('./src/UserDto')
const PositionDto = require('./src/PositionDto')
const PositionUpsertDto = require('./src/PositionUpsertDto')
const PositionStatusDto = require('./src/PositionStatusDto')
const PositionSkillDto = require('./src/PositionSkillDto')


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
    PositionUpsertDto
}