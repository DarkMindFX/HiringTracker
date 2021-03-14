
const routeHealth = require('./health');


function initRoutes(router) {
    routeHealth(router);
}

module.exports = initRoutes;