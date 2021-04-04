
const { Error, SkillDto } = require('hrt.dto');
const { SkillDal, SkillEntity } = require('hrt.dal')
const { Converter } = require('../coverters')
const { prepInitParams } = require('../dalHelper')

const constants = require('../constants');

function routeSkills(route) {
    route.get('/api/v1/skills', (req, res) => { getSkills(req, res); })
}

async function getSkills(req, res) {
    try {
        
        let initParams = prepInitParams();
        let dal = new SkillDal();
        dal.init(initParams);
        let profs = await dal.GetAll();

        let dtos = [];

        for(let i in profs) {
            dtos.push(Converter.skillEntity2Dto( profs[i] ))
        }

        res.status(constants.HTTP_OK);
        res.send(JSON.stringify(dtos, null, 4));
    }
    catch(error) {
        console.error('Error processing GET skills request', error.message);
        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = `Error processing GET skills request: ${error.message}`;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);
    }    
}

module.exports = routeSkills;