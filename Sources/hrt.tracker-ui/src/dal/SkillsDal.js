


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class SkillsDal extends DalBase {

    constructor() {
        super();
    }

    async insertSkill(newSkill) {
        let inst = this.Instance;

        try {
            let res = await inst.put(`/skills`, newSkill);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateSkill(updatedSkill) {
        let inst = this.Instance;
        
        try {
            let res = await inst.post(`/skills`, updatedSkill);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteSkill(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/skills/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getSkills()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/skills`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getSkill(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/skills/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = SkillsDal;