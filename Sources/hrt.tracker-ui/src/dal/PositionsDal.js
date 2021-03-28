

const axios = require('axios')
const constants = require('../constants');
const DalBase = require('./DalBase');

class PositionsDal extends DalBase {

    constructor() {
        super();
        this._positions = null;        
    }

    async loadPositions() {

        const url = this.ApiUrl;
       
        let inst = axios.create({
            baseURL: url
        })

        let res = await inst.get(`/positions`);

        this._positions = res.data;
    }

    async getPositions() {

        // TODO: add cache
        await this.loadPositions();        

        return this._positions;
    }

    async getPosition(id) {
        const url = this.ApiUrl;
       
        let inst = axios.create({
            baseURL: url
        })

        let res = await inst.get(`/positions/${id}`);

        let position = res.data;

        return position;
    }

    async getPositionSkills(id) {
        const url = this.ApiUrl;
       
        let inst = axios.create({
            baseURL: url
        })

        let res = await inst.get(`/positions/${id}/skills`);

        let position = res.data;

        return position;
    }
}

module.exports = PositionsDal;