
const { Error } = require('hrt.dto');
const { PositionCandidateStepDal } = require('hrt.dal')
const { DalHelper } = require('../dalHelper');
const { Converter } = require('../coverters');
const { authUserOnly } = require('../middleware');

const constants = require('../constants');

function routePositionCandidateSteps(route) {
    route.get('/api/v1/position_candidate_steps', authUserOnly(), (req, res) => { getPositionCandidateStepes(req, res); });
}

async function getPositionCandidateStepes(req, res) {
    try {

        let dal = _getPositionCandidateStepDal();

        let entites = await dal.GetAll();   
        
        let dtos = entites.map( e => { return Converter.positionCandidateStepEntity2Dto(e) } );

        res.status(constants.HTTP_OK);
        res.send(JSON.stringify(dtos, null, 4));
    }
    catch(error) {
        let msg = `Error processing GET position-candidate steps request: ${error.message}`;
        console.error(msg);
        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = msg;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);
    }
}

function _getPositionCandidateStepDal() {
    let initParams = DalHelper.prepInitParams();
    let dal = new PositionCandidateStepDal();
    dal.init(initParams);

    return dal;
}

module.exports = routePositionCandidateSteps;