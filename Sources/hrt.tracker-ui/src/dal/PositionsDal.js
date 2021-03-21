

const axios = require('axios')
const constants = require('../constants');

class PositionsDal {

    constructor() {
        this._positions = null;        
    }

    async loadPositions() {

        const url = `${constants.HRT_API_HOST}/api/${constants.HRT_API_VERSION}`;

        console.log(url);
        
        let inst = axios.create({
            baseURL: url
        })

        let res = await inst.get(`/positions`);

        this._positions = res.data
    }

    async getPositions() {

        if(!this._positions) {
            await this.loadPositions();
        }

        return this._positions;
    }
}
module.exports = PositionsDal;