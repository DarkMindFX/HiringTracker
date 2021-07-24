


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class PositionSkillsDal extends DalBase {

    constructor() {
        super();
    }

    async insertPositionSkill(newPositionSkill) {
        let inst = this.Instance;

        try {
            let res = await inst.put(`/positionskills`, newPositionSkill);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updatePositionSkill(updatedPositionSkill) {
        let inst = this.Instance;
        
        try {
            let res = await inst.post(`/positionskills`, updatedPositionSkill);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deletePositionSkill(positionid,skillid) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/positionskills/${positionid}/${skillid}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getPositionSkills()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/positionskills`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getPositionSkill(positionid,skillid) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/positionskills/${positionid}/${skillid}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getPositionSkillsByPosition(positionid)
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/positionskills/byposition/${positionid}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }        
    }
}

module.exports = PositionSkillsDal;