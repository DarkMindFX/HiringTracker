
const express = require('express');
const initRoutes = require('./routes');
const constants = require('./constants');
const version = require('../package.json').version;

function initExpressApp() {
    let app = express();
    let port = constants.PORT;

    var router = express.Router()

    initRoutes(router);

    app.get('/', (req, res) => {
        res.status(constants.HTTP_OK);
        res.send(version);
    });

    app.use(router);

    app.listen(port, () => {
        console.log(`Listening to http://localhost:${port}/`);
    });

    return app;
}

module.exports = initExpressApp;