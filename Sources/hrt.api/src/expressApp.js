
const express = require('express');
const bodyParser = require('body-parser');
const cors = require('cors')
const initRoutes = require('./routes');
const { validateAuthToken, jsonResponseContentType } = require('./middleware');
const constants = require('./constants');
const version = require('../package.json').version;

function initExpressApp() {
    let app = express();
    let port = constants.PORT;

    app.use(cors());
    app.use(bodyParser.urlencoded({ extended: true }));
    app.use(bodyParser.json());
    app.use(bodyParser.raw());

    app.use(validateAuthToken());
    app.use(jsonResponseContentType())

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