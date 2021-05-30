

const axios = require('axios')
const constants = require('../constants');

const DalBase = require('./DalBase');

class UtilsDal extends DalBase {

    constructor()
    {
        super();
        this._posCandidateStatuses = null;
        this._posCandidateSteps = null;
    }

    async loadPositionCandidateSteps()
    {
        let inst = this.Instance;

        let res = await inst.get(`/positioncandidatesteps`);

        this._posCandidateSteps = {};
        res.data.forEach( r => {
            this._posCandidateSteps[r._stepId] = {
                StepID: r._stepId,
                Name: r._name,
                ReqDueDate: r._reqDueDate,
                RequiresRespInDays: r._requiresRespInDays
            }
        })
    }

    async loadPositionCandidateStatuses()
    {
        let inst = this.Instance;

        let res = await inst.get(`/positioncandidatestatuses`);

        this._posCandidateStatuses = {};
        res.data.forEach( r => {
            this._posCandidateStatuses[r._statusId] = {
                StatusID: r._statusId,
                Name: r._name
            }
        })
    }

    async getPositionCandidateStepsAsTable()
    {
        if(!this._posCandidateSteps) {
            await this.loadPositionCandidateSteps();
        }

        return this._posCandidateSteps;        
    }

    async getPositionCandidateSteps()
    {
        if(!this._posCandidateSteps) {
            await this.loadPositionCandidateSteps();
        }

        return Object.values(this._posCandidateSteps);
    }

    async getPositionCandidateStatuses()
    {
        if(!this._posCandidateStatuses) {
            await this.loadPositionCandidateStatuses();
        }

        return Object.values(this._posCandidateStatuses);
    }

    async getPositionCandidateStatusesAsTable()
    {
        if(!this._posCandidateStatuses) {
            await this.loadPositionCandidateStatuses();
        }

        return this._posCandidateStatuses;
    }

}

module.exports = UtilsDal;