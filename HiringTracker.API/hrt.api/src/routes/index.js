
const routeHealth = require('./health');
const routeSkillProficiency = require('./skill_proficiency')


function initRoutes(router) {
    routeHealth(router);
    routeSkillProficiency(router);
}

module.exports = initRoutes;