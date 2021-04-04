
const { PositionStatusDal, UserDal, PositionDal, SkillDal, SkillPoficiencyDal } = require('hrt.dal');
const { Converter } = require('./coverters');
const constants = require('./constants');

function prepInitParams() {
    let initParams = {}
    initParams['server'] = constants.SQL_SERVER;
    initParams['database'] = constants.SQL_DB;
    initParams['username'] = constants.SQL_USER;
    initParams['password'] = constants.SQL_PWD;
    initParams['encrypt'] = constants.SQL_ENCRYPT;

    return initParams;    
}

async function getUsersDto()
{
    let initParams = prepInitParams();
    let dalUser = new UserDal();
    await dalUser.init(initParams);
    let users = await dalUser.GetAll();
    let dtos = {};
    for(let i in users) {

        let user = users[i];
        let dto = Converter.userEntity2Dto(user); 
        
        dtos[dto.UserID] = dto;
    }

    return dtos;
}

async function getPositionStatusesDto()
{
    let initParams = prepInitParams();
    let dalPosStatus = new PositionStatusDal();
    await dalPosStatus.init(initParams);
    let statuses = await dalPosStatus.GetAll();
    let dtos = {};
    for(let i in statuses) {

        let status = statuses[i];
        let dto = Converter.positionStatusEntity2Dto(status); 
        
        dtos[dto.StatusID] = dto;
    }

    return dtos;
}

async function getPositionsDto()
{
    let dictUsers = await getUsersDto();
    let dicStatuses = await getPositionStatusesDto();

    let initParams = prepInitParams();
    let dalPosStatus = new PositionDal();
    await dalPosStatus.init(initParams);
    let positions = await dalPosStatus.GetAll();
    let dtos = {};
    for(let i in positions) {

        let pos = positions[i];
        let dto = Converter.positionEntity2Dto(pos, dictUsers, dicStatuses); 
        
        dtos[dto.PositionID] = dto;
    }

    return dtos;
}

async function getSkillsDto() {
    let initParams = prepInitParams();
    let dal = new SkillDal();
    await dal.init(initParams);

    let skills = await dal.GetAll();

    let dtos = {};

    skills.forEach(skill => {
        dtos[skill.SkillID] = Converter.skillEntity2Dto(skill);
    });

    return dtos;
}

async function getSkillProficiencyDto() {
    let initParams = prepInitParams();
    let dal = new SkillPoficiencyDal();
    await dal.init(initParams);

    let profs = await dal.GetAll();

    let dtos = {};

    profs.forEach(p => {
        dtos[p.ProficiencyID] = Converter.skillProficiencyEntity2Dto(p);
    });

    return dtos;
}

module.exports = {
    prepInitParams,
    getUsersDto,
    getPositionStatusesDto,
    getPositionsDto,
    getSkillsDto,
    getSkillProficiencyDto
}