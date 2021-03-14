
const { Error, HealthResponse } = require('hrt.dto');

const constants = require('../constants');

function routeHealth(router) {
    router.get('/api/v1/health', (req, res) => { getHealth(req, res); })
}

function getHealth(req, res) {
    try {
        let respBody = new HealthResponse();
        respBody.message = "Health is OK";

        res.status(constants.HTTP_OK);
        res.send(respBody);
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

module.exports = routeHealth;