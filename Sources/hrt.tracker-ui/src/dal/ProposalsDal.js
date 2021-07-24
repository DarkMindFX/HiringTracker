


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class ProposalsDal extends DalBase {

    constructor() {
        super();
    }

    async insertProposal(newProposal) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/proposals`, newProposal);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateProposal(updatedProposal) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/proposals`, updatedProposal);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteProposal(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/proposals/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getProposals()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/proposals`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getProposal(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/proposals/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = ProposalsDal;