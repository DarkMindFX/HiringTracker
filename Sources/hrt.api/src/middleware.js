
const jwt = require('jsonwebtoken');
const { prepInitParams } = require('../src/dalHelper')
const { UserDal, UserEntity } = require('hrt.dal')
const constants = require('./constants')

function userValidation() {
    let middleware = function (req, res, next) {
        let token = req.headers['authorization'];
        if(token) {
            token = token.substring( "Bearer ".length, token.length);
            jwt.verify(token, constants.SESSION_SECRET, function(err, decoded) {
                if (!err) 
                {
                    const userId = decoded.id;

                    let initParams = prepInitParams();
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

module.exports = {
    userValidation
}