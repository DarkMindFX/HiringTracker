

const constants = {
    PORT: 8082,
    
    SQL_USER: process.env.SQL_USER || 'sa',
    SQL_AUTH_TYPE: process.env.SQL_AUTH_TYPE || 'default',
    SQL_PWD: process.env.SQL_PWD || 'Indahouse000',
    SQL_SERVER: process.env.SQL_SERVER || 'localhost\\SQLEXPRESS',
    SQL_DB: process.env.SQL_DB || 'HiringTracker',
    SQL_ENCRYPT: process.env.SQL_ENCRYPT || false,

    PWD_SALT_LENGTH: 7,
    SESSION_SECRET: 'QDFGBDFH456DFSD',
    SESSION_TIMEOUT: 600,

    HTTP_OK: 200,
    HTTP_NoContent: 204,
    HTTP_BadRequest: 400,
    HTTP_Unauthorized: 401,
    HTTP_Forbidden: 403,
    HTTP_NotFound: 404,
    HTTP_IntServerError: 500,
    HTTP_NotImplemented: 501
}

module.exports = constants;