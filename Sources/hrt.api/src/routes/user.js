
const jwt = require('jsonwebtoken');

const { Error, UserDto, LoginResponse } = require('hrt.dto');
const { UserDal, UserEntity } = require('hrt.dal')

const { prepInitParams, getUsersDto } = require('../dalHelper')
const { UsersHelper } = require('../usersHelper')

const constants = require('../constants');
const { Converter } = require('../coverters');

function routeUsers(route) {
    route.post('/api/v1/login', (req, res) => { loginUser(req, res); })
    route.get('/api/v1/users', (req, res) => { getUsers(req, res); })
    route.get('/api/v1/users/:id', (req, res) => { getUserById(req, res); })
    route.put('/api/v1/users', (req, res) => { createUser(req, res); })
    route.post('/api/v1/users', (req, res) => { updateUser(req, res); })    
    route.delete('/api/v1/users', (req, res) => { deleteUser(req, res); })
}

async function createUser(req, res)
{
    try {
        let helper = new UsersHelper();
        let entity = Converter.userDto2Entity(req.body);
        entity.Salt = helper.generateSalt(constants.PWD_SALT_LENGTH);
        entity.PwdHash = helper.getPasswordHash(req.body._pwd, entity.Salt);

        let initParams = prepInitParams();
        let dal = new UserDal();
        dal.init(initParams);

        const result = await dal.Upsert(entity);

        const userId = parseInt(result['NewUserID'])

        let user = await dal.GetDetails(userId) 
        if(user) {

            let dto = Converter.userEntity2Dto(user);

            res.status(constants.HTTP_OK);
            res.send(dto);
        }
        else {
            throw {
                message: `Unexpected error: cannot find user which was just inserted - [id:${userId}]`
            }            
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
        let initParams = prepInitParams();
        let dal = new UserDal();
        dal.init(initParams);

        let entity = Converter.userDto2Entity(req.body);

        let user = await dal.GetDetails(entity.UserID)
        if(user) {
            

        }
        else {
            res.status(constants.HTTP_NotFound);
            let errBody = new Error();
            errBody.message = `User [id: ${req.body._userId}] not found`;
            errBody.code = constants.HTTP_NotFound;
            res.send(erroBody);
        }


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
        let helper = new UsersHelper();
        
        const login = req.body._login;
        const pwd = req.body._pwd;

        let initParams = prepInitParams();
        let dal = new UserDal();
        dal.init(initParams);

        const user = await dal.GetDetailsByLogin(login);
        if(user) {

            const pwdHash = helper.getPasswordHash(pwd, user.Salt);

            if(pwdHash === user.PwdHash) {
                // logged in successfully

                const expires = new Date(Date.now())
                expires.setSeconds(expires.getSeconds() + constants.SESSION_TIMEOUT);

                var token = jwt.sign({ id: user.UserID }, constants.SESSION_SECRET, {
                    expiresIn: constants.SESSION_TIMEOUT 
                });

                let dto = new LoginResponse();
                dto.Token = token;
                dto.Expires = expires;

                res.status(constants.HTTP_OK);
                res.send(dto);
            }
            else {
                res.status(constants.HTTP_Forbidden);
                let errBody = new Error();
                errBody.message = `Login failed. Invalid login/password combination.`;
                errBody.code = constants.HTTP_Forbidden;
                res.send(errBody);                
            }
        }
        else {
            res.status(constants.HTTP_NotFound);
            let errBody = new Error();
            errBody.message = `User [login: ${req.body._login}] not found`;
            errBody.code = constants.HTTP_NotFound;
            res.send(erroBody);
        }

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