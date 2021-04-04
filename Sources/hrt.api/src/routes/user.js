
const { Error, UserDto } = require('hrt.dto');
const { UserDal, UserEntity } = require('hrt.dal')

const { prepInitParams, getUsersDto } = require('../dalHelper')
const { UsersHelper } = require('../usersHelper')

const constants = require('../constants');
const { Converter } = require('../coverters');

function routeUsers(route) {
    route.get('/api/v1/users', (req, res) => { getUsers(req, res); })
    route.get('/api/v1/users/:id', (req, res) => { getUserById(req, res); })
    route.put('/api/v1/users', (req, res) => { createUser(req, res); })
    route.post('/api/v1/users', (req, res) => { updateUser(req, res); })
    route.delete('/api/v1/users', (req, res) => { deleteUser(req, res); })
}

async function createUser(req, res)
{
    try {

        let entity = Converter.userDto2Entity(req.body);
        entity.Salt = UsersHelper.generateSalt(constants.PWD_SALT_LENGTH);
        entity.PwdHash = UsersHelper.getPasswordHash(req.body._pwd, entity.Salt);
    }
    catch(error) {
        const errMsg = `Error processing create user request: ${error.message}`
        console.error(errMsg);

        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = errMsg;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);
    }   
}

async function deleteUser(req, res)
{
    try {
    }
    catch(error) {
        const errMsg = `Error processing delete user request: ${error.message}`
        console.error(errMsg);

        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = errMsg;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);
    }   
}

async function updateUser(req, res)
{
    try {
    }
    catch(error) {
        const errMsg = `Error processing update user request: ${error.message}`
        console.error(errMsg);

        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = errMsg;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);
    }   
}

async function loginUser(req, res)
{
    try {
    }
    catch(error) {
        const errMsg = `Error processing login user request: ${error.message}`
        console.error(errMsg);

        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = errMsg;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);
    }   
}

async function getUserById(req, res)
{
    try {

        let initParams = prepInitParams();
        let dal = new UserDal();
        dal.init(initParams);

        let userId = parseInt(req.params.id);
        let user = await dal.GetDetails(userId) 
        if(user) {

            let dto = Converter.userEntity2Dto(user);

            res.status(constants.HTTP_OK);
            res.send(dto);

        }
        else {
            res.status(constants.HTTP_NotFound);
            let errBody = new Error();
            errBody.message = `User [id: ${req.params.id}] not found`;
            errBody.code = constants.HTTP_NotFound;
            res.send(erroBody);
        }
    }
    catch(error) {
        const errMsg = `Error processing create user request: ${error.message}`
        console.error(errMsg);

        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = errMsg;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);
    }   
}

async function getUsers(req, res) {
    try {

        let dtos = Object.values(await getUsersDto());

        res.status(constants.HTTP_OK);
        res.send(JSON.stringify(dtos, null, 4));
    }
    catch(error) { 
        const errMsg = `Error processing GET users request: ${error.message}`;
        console.error(errMsg);

        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = errMsg;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);
    }    
}

module.exports = routeUsers;