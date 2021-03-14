
const constants = {
    SQL_USER: process.env.SQL_USER || 'sa',
    SQL_AUTH_TYPE: process.env.SQL_AUTH_TYPE || 'default',
    SQL_PWD: process.env.SQL_PWD || 'Indahouse000',
    SQL_SERVER: process.env.SQL_SERVER || 'localhost\\SQLEXPRESS',
    SQL_DB: process.env.SQL_DB || 'HiringTracker',
    SQL_ENCRYPT: process.env.SQL_ENCRYPT || false
}

module.exports = {
    constants
}