const { constants } = require('./constants')
var fs = require('fs');
const mssql = require('mssql');

function prepInitParams() {
    let initParams = {}
    initParams['server'] = constants.SQL_SERVER;
    initParams['database'] = constants.SQL_DB;
    initParams['username'] = constants.SQL_USER;
    initParams['password'] = constants.SQL_PWD;
    initParams['encrypt'] = constants.SQL_ENCRYPT;

    return initParams;    
}

async function execSetup(root, testName) {
    let filename = 'Setup.sql'
    let fullPath = root + '\\' + testName + '\\' + filename;

    if(fs.existsSync(fullPath)) {
        execFileScript(fullPath);
    }
}

async function execTeardown(root, testName) {
    let filename = 'Teardown.sql'
    let fullPath = root + '\\' + testName + '\\' + filename;

    if(fs.existsSync(fullPath)) {
        execFileScript(fullPath);
    }   
}

async function execFileScript(path) {
    let sql = fs.readFileSync(path, 'utf8');
    
    if(sql.length > 0) {
        let initParams = prepInitParams();
        let config = {
            user: initParams['username'],
            password: initParams['password'],
            server: initParams['server'],
            database: initParams['database']        
        }

        await mssql.connect(config);

        let request = new mssql.Request();

        request.query(sql);
    }
}

module.exports = {
    prepInitParams,
    execSetup,
    execTeardown
}