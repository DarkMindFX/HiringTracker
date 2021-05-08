
const { PositionStatusDal, UserDal, PositionDal, SkillDal, SkillPoficiencyDal, CandidateDal } = require('hrt.dal');
const { Converter } = require('./coverters');
const constants = require('./constants');

class DalHelper 
{

    static prepInitParams() {
        let initParams = {}
        initParams['server'] = constants.SQL_SERVER;
        initParams['database'] = constants.SQL_DB;
        initParams['username'] = constants.SQL_USER;
        initParams['password'] = constants.SQL_PWD;
        initParams['encrypt'] = constants.SQL_ENCRYPT;

        return initParams;    
    }

    static async getUsersDto()
    {
        let initParams = DalHelper.prepInitParams();
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

    static async getPositionStatusesDto()
    {
        let initParams = DalHelper.prepInitParams();
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

    static async getPositionsDto()
    {
        let dictUsers = await DalHelper.getUsersDto();
        let dicStatuses = await DalHelper.getPositionStatusesDto();

        let initParams = DalHelper.prepInitParams();
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

    static async getCandidatesDto() {

        let dictUsers = await DalHelper.getUsersDto();

        let initParams = DalHelper.prepInitParams();
        let dalCandidates = new CandidateDal();
        await dalCandidates.init(initParams);

        let candidates = await dalCandidates.GetAll();

        let dtos = {};
        for(let i in candidates) {
            let c = candidates[i];
            let dto = Converter.candidateEntity2Dto(c, dictUsers); 
            
            dtos[dto.CandidateID] = dto;
        }

        return dtos;        
    }

    static async getSkillsDto() {
        let initParams = DalHelper.prepInitParams();
        let dal = new SkillDal();
        await dal.init(initParams);

        let skills = await dal.GetAll();

        let dtos = {};

        skills.forEach(skill => {
            dtos[skill.SkillID] = Converter.skillEntity2Dto(skill);
        });

        return dtos;
    }

    static async getSkillProficiencyDto() {
        let initParams = DalHelper.prepInitParams();
        let dal = new SkillPoficiencyDal();
        await dal.init(initParams);

        let profs = await dal.GetAll();

        let dtos = {};

        profs.forEach(p => {
            dtos[p.ProficiencyID] = Converter.skillProficiencyEntity2Dto(p);
        });

        return dtos;
    }

}

module.exports = { DalHelper };