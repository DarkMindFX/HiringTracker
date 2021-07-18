


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class ProposalStatusesDal extends DalBase {

    constructor() {
        super();
    }

    async insertProposalStatus(newProposalStatus) {
        let inst = this.Instance;

        try {
            let res = await inst.put(`/proposalstatuses`, newProposalStatus);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateProposalStatus(updatedProposalStatus) {
        let inst = this.Instance;
        
        try {
            let res = await inst.post(`/proposalstatuses`, updatedProposalStatus);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteProposalStatus(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/proposalstatuses/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getProposalStatuses()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/proposalstatuses`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getProposalStatus(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/proposalstatuses/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = ProposalStatusesDal;