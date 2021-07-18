


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class ProposalStepsDal extends DalBase {

    constructor() {
        super();
    }

    async insertProposalStep(newProposalStep) {
        let inst = this.Instance;

        try {
            let res = await inst.put(`/proposalsteps`, newProposalStep);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateProposalStep(updatedProposalStep) {
        let inst = this.Instance;
        
        try {
            let res = await inst.post(`/proposalsteps`, updatedProposalStep);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteProposalStep(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/proposalsteps/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getProposalSteps()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/proposalsteps`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getProposalStep(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/proposalsteps/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = ProposalStepsDal;