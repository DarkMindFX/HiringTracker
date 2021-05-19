
const { Error } = require('hrt.dto');
const { PositionCandidateStatusDal } = require('hrt.dal')
const { DalHelper } = require('../dalHelper');
const { Converter } = require('../coverters');
const { authUserOnly } = require('../middleware');

const constants = require('../constants');

function routePositionCandidateStatuses(route) {
    route.get('/api/v1/position_candidate_statuses', authUserOnly(), (req, res) => { getPositionCandidateStatuses(req, res); });
}

async function getPositionCandidateStatuses(req, res) {
    try {

        let dal = _getPositionCandidateStatusDal();

        let entites = await dal.GetAll();   
        
        let dtos = entites.map( e => { return Converter.positionCandidateStatusEntity2Dto(e) } );

        res.status(constants.HTTP_OK);
        res.send(JSON.stringify(dtos, null, 4));
    }
    catch(error) {
        let msg = `Error processing GET position-candidate statuses request: ${error.message}`;
        console.error(msg);
        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = msg;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);
    }
}

function _getPositionCandidateStatusDal() {
    let initParams = DalHelper.prepInitParams();
    let dal = new PositionCandidateStatusDal();
    dal.init(initParams);

    return dal;
}

module.exports = routePositionCandidateStatuses;