
const constants = {
    SQL_USER: process.env.SQL_USER || 'sa',
    SQL_AUTH_TYPE: process.env.SQL_AUTH_TYPE || 'default',
    SQL_PWD: process.env.SQL_PWD || 'Indahouse000',
    SQL_SERVER: process.env.SQL_SERVER || 'localhost\\SQLEXPRESS',
    SQL_DB: process.env.SQL_DB || 'HiringTracker',
    SQL_ENCRYPT: process.env.SQL_ENCRYPT || false,

    USER_ID_JOEB : 100001,
    USER_ID_DONALDT : 100002,
    USER_ID_BARAKO : 100003,

    PROF_ID_BEGINNER : 1,
    PROF_ID_INTERMEDIATE : 2,
    PROF_ID_ADVANCED : 3,
    PROF_ID_EXPERT : 4,

    POSSTATUS_ID_DRAFT : 1,
    POSSTATUS_ID_OPEN : 2,
    POSSTATUS_ID_HOLD : 3,
    POSSTATUS_ID_CLOSED : 4,
    POSSTATUS_ID_CANCELLED : 5
}

module.exports = {
    constants
}