

const axios = require('axios')
const constants = require('../constants');
const DalBase = require('./DalBase');

class PositionsDal extends DalBase {

    constructor() {
        super();        
    }

    async getPositions() {

        try {
            let inst = this.Instance;
        
            let res = await inst.get(`/positions`);

            return res;
        }
        catch(error) {
            console.log(error.response);
            throw error;
        }
    }

    async getPosition(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/positions/${id}`);

            return res;
        }
        catch(error) {
            console.log(error);
            throw error;
        }
    }

    async getPositionSkills(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/positions/${id}/skills`);

            return res;
        }
        catch(error) {
            console.log(error.response);
            throw error;
        }
    }
}

module.exports = PositionsDal;