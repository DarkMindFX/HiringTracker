
const axios = require('axios')
const constants = require('../constants');
const DalBase = require('./DalBase');

class CandidatesDal extends DalBase {

    constructor() {
        super();        
    }

    async getCandidates() {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/candidates`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getCandidateSkills(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/candidates/${id}/skills`);

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