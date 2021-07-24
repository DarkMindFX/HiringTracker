


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class CandidatesDal extends DalBase {

    constructor() {
        super();
    }

    async insertCandidate(newCandidate) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/candidates`, newCandidate);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateCandidate(updatedCandidate) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/candidates`, updatedCandidate);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteCandidate(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/candidates/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getCandidates()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/candidates`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getCandidate(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/candidates/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = CandidatesDal;