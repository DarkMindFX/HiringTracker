
const { Error, PositionDto } = require('hrt.dto');
const { PositionDal, PositionEntity, UserDal, UserEntity } = require('hrt.dal')
const { prepInitParams, getPositionsDto } = require('../dalHelper')

const constants = require('../constants');

function routePositions(route) {
    route.get('/api/v1/positions', (req, res) => { getPositions(req, res); })
}

async function getPositions(req, res) {
    try {

        let dtos = Object.values( await getPositionsDto() );        

        res.status(constants.HTTP_OK);
        res.send(JSON.stringify(dtos, null, 4));
    }
    catch(error) {
        console.error('Error processing GET positions request', error.message);
        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = `Error processing GET positions request: ${error.message}`;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);
    }
}

module.exports = routePositions;