
const constants = require('../constants');

class DalBase {

    get ApiUrl() {

        const url = `${constants.HRT_API_HOST}/api/${constants.HRT_API_VERSION}`;
        
        return url;
    }

}

module.exports = DalBase;