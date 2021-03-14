const initExpressApp = require('./src/expressApp')

function start() {
    let server = initExpressApp();

    return server;
}

let server = start();

module.exports = server;