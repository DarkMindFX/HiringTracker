


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class CandidateSkillsDal extends DalBase {

    constructor() {
        super();
    }

    async insertCandidateSkill(newCandidateSkill) {
        let inst = this.Instance;

        try {
            let res = await inst.put(`/candidateskills`, newCandidateSkill);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateCandidateSkill(updatedCandidateSkill) {
        let inst = this.Instance;
        
        try {
            let res = await inst.post(`/candidateskills`, updatedCandidateSkill);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteCandidateSkill(candidateid,skillid) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/candidateskills/${candidateid}/${skillid}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getCandidateSkills()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/candidateskills`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getCandidateSkill(candidateid,skillid) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/candidateskills/${candidateid}/${skillid}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = CandidateSkillsDal;