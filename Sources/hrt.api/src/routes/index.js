
const routeHealth = require('./health');
const routeSkillProficiency = require('./skill_proficiency')
const routePositions = require('./position')
const routeUsers = require('./user')
const routeSkills = require('./skill')


function initRoutes(router) {
    routeHealth(router);
    routeSkillProficiency(router);
    routePositions(router);
    routeUsers(router);
    routeSkills(router);
}

module.exports = initRoutes;