
const { Error, SkillProficiencyDto } = require('hrt.dto');
const { SkillPoficiencyDal, SkillPoficiencyEntity } = require('hrt.dal')
const { Converter } = require('../coverters')
const { DalHelper } = require('../dalHelper')

const constants = require('../constants');

function routeSkillProficiency(route) {
    route.get('/api/v1/skillproficiencies', (req, res) => { getSkillProficiencies(req, res); })
}

async function getSkillProficiencies(req, res) {
    try {
        
        let initParams = DalHelper.prepInitParams();
        let dal = new SkillPoficiencyDal();
        dal.init(initParams);
        let profs = await dal.GetAll();

        let dtos = [];

        for(let i in profs) {
            dtos.push(Converter.skillProficiencyEntity2Dto( profs[i] ))
        }

        res.status(constants.HTTP_OK);
        res.send(JSON.stringify(dtos, null, 4));
    }
    catch(error) {
        console.error('Error processing GET skillproficiencies request', error.message);
        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = `Error processing GET skillproficiencies request: ${error.message}`;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);
    }    
}





module.exports = routeSkillProficiency;