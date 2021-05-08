
const jwt = require('jsonwebtoken');
const { DalHelper } = require('../src/dalHelper')
const { UserDal, UserEntity } = require('hrt.dal')
const { Error } = require('hrt.dto')
const constants = require('./constants')

function validateAuthToken() {
    let middleware = function (req, res, next) {
        let token = req.headers['authorization'];
        if(token) {
            token = token.substring( "Bearer ".length, token.length);
            jwt.verify(token, constants.SESSION_SECRET, function(err, decoded) {
                if (!err) 
                {
                    const userId = decoded.id;

                    let initParams = DalHelper.prepInitParams();
                    let dal = new UserDal();
                    dal.init(initParams);

                    dal.GetDetails(userId).then( (user) => {
                        if(user) {
                            if(!req.middleware) req.middleware = {}
                            req.middleware.user = user;
                        }
                        next();
                    } );
                }
                else {
                    next();
                }
                            
            });
        }
        else {
            next();            
        }
        
    }

    return middleware;
}

function authUserOnly() {
    let middleware = function (req, res, next) {
        if(!req.middleware || !req.middleware.user) {
            
            let error = new Error()
            error.code = constants.HTTP_Unauthorized;
            error.message = "Operation unauthorized. No session token provided."

            res.status(constants.HTTP_Unauthorized)
            res.send(error);
        }
        else {
            next();
        }
    }

    return middleware;
}

module.exports = {
    validateAuthToken,
    authUserOnly
}