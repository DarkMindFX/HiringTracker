
const routeHealth = require('./health');
const routeSkillProficiency = require('./skill_proficiency')
const routePositions = require('./position')
const routePositionCandidateStatuses = require('./position_candidate_status')
const routePositionCandidateSteps = require('./position_candidate_step')
const routeUsers = require('./user')
const routeSkills = require('./skill')
const routeCandidates = require('./candidate')


function initRoutes(router) {
    routeHealth(router);
    routeSkillProficiency(router);
    routePositions(router);
    routePositionCandidateStatuses(router);
    routePositionCandidateSteps(router);    
    routeUsers(router);
    routeSkills(router);
    routeCandidates(router);
}

module.exports = initRoutes;