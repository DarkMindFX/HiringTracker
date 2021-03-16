
const { Error, SkillProficiencyDto } = require('hrt.dto');
const { SkillPoficiencyDal, SkillPoficiencyEntity } = require('hrt.dal')
const { skillProficiencyEntity2Dto } = require('./coverters')

const constants = require('../constants');

function routeSkillProficiency(route) {
    route.get('/api/v1/skillproficiencies', (req, res) => { getSkillProficiencies(req, res); })
}

async function getSkillProficiencies(req, res) {
    try {
        
        let initParams = prepInitParams();
        let dal = new SkillPoficiencyDal();
        dal.init(initParams);
        let profs = await dal.GetAll();

        let dtos = [];

        for(let i in profs) {
            dtos.push(skillProficiencyEntity2Dto( profs[i] ))
        }

        res.status(constants.HTTP_OK);
        res.send(dtos);
    }
    catch(error) {
        console.error('Error processing health request', error.message);
        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = `Error processing health request: ${error.message}`;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);
    }    
}



function prepInitParams() {
    let initParams = {}
    initParams['server'] = constants.SQL_SERVER;
    initParams['database'] = constants.SQL_DB;
    initParams['username'] = constants.SQL_USER;
    initParams['password'] = constants.SQL_PWD;
    initParams['encrypt'] = constants.SQL_ENCRYPT;

    return initParams;    
}

module.exports = routeSkillProficiency;