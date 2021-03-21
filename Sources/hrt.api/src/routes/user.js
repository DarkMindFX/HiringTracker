
const { Error, UserDto } = require('hrt.dto');
const { UserDal, UserEntity } = require('hrt.dal')

const { getUsersDto } = require('../dalHelper')

const constants = require('../constants');

function routeUsers(route) {
    route.get('/api/v1/users', (req, res) => { getUsers(req, res); })
}

async function getUsers(req, res) {
    try {

        let dtos = Object.values(await getUsersDto());

        res.status(constants.HTTP_OK);
        res.send(JSON.stringify(dtos, null, 4));
    }
    catch(error) {
        console.error('Error processing GET users request', error.message);
        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = `Error processing GET users request: ${error.message}`;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);
    }    
}

module.exports = routeUsers;