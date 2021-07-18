


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class SkillProficienciesDal extends DalBase {

    constructor() {
        super();
    }

    async insertSkillProficiency(newSkillProficiency) {
        let inst = this.Instance;

        try {
            let res = await inst.put(`/skillproficiencies`, newSkillProficiency);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateSkillProficiency(updatedSkillProficiency) {
        let inst = this.Instance;
        
        try {
            let res = await inst.post(`/skillproficiencies`, updatedSkillProficiency);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteSkillProficiency(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/skillproficiencies/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getSkillProficiencies()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/skillproficiencies`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getSkillProficiency(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/skillproficiencies/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = SkillProficienciesDal;