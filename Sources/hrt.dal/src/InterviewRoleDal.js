
const { SQLDal } = require('./SQLDal');
const InterviewRoleEntity = require('./entities/InterviewRole');
const mssql = require('mssql');

class InterviewRoleDal extends SQLDal {

    init(initParams) {
        super.initDal(initParams);        
    }

    async GetByInterview(interviewId) {
        let intRoles = null;
        await mssql.connect(this._config);

        let inParams = {};
        inParams['InterviewID'] = { value: interviewID, type: mssql.BigInt } 

        let records = await super.execStorProcRecordset(mssql, 'p_InterviewRole_GetByInterview', inParams);

        if(records) {
            intRoles = [];
            for(let r in records) {
                let intRole = this.recordToEntity(records[r]);               

                intRoles.push(intRole);
            }
        }

        return intRoles;
    }

    async GetByUser(userId) {
        let intRoles = null;
        await mssql.connect(this._config);

        let inParams = {};
        inParams['UserID'] = { value: userId, type: mssql.BigInt } 

        let records = await super.execStorProcRecordset(mssql, 'p_InterviewRole_GetByUser', inParams);

        if(records) {
            intRoles = [];
            for(let r in records) {
                let intRole = this._recordToEntity(records[r]);               

                intRoles.push(intRole);
            }
        }

        return intRoles;
    }

    async GetByRole(roleId) {
        let intRoles = null;
        await mssql.connect(this._config);

        let inParams = {};
        inParams['RoleID'] = { value: roleId, type: mssql.BigInt } 

        let records = await super.execStorProcRecordset(mssql, 'p_InterviewRole_GetByRole', inParams);

        if(records) {
            intRoles = [];
            for(let r in records) {
                let intRole = this._recordToEntity(records[r]);               

                intRoles.push(intRole);
            }
        }

        return intRoles;
    }

    async AssignUser(interviewId, userId, roleId) {

        await mssql.connect(this._config);

        let inParams = {};
        inParams['InterviewID'] = { value: interviewId, type: mssql.BigInt }
        inParams['UserID'] = { value: userId, type: mssql.BigInt }
        inParams['RoleID'] = { value: roleId, type: mssql.BigInt } 

        return super.execStorProcRecordset(mssql, 'p_InterviewRole_UpsertUserRole', inParams);
    }

    async UnassignUser(interviewId, userId) {

        await mssql.connect(this._config);

        let inParams = {};
        inParams['InterviewID'] = { value: interviewId, type: mssql.BigInt }
        inParams['UserID'] = { value: userId, type: mssql.BigInt }
   
        return super.execStorProcRecordset(mssql, 'p_InterviewRole_RemoveUserRole', inParams);
    }

    _recordToEntity(record)
    {
        let intRole = new InterviewRoleEntity();

        intRole.InterviewID = parseInt(record.StatusID);
        intRole.UserID = parseInt(record.UserID);
        intRole.RoleID = parseInt(record.RoleID);

        return intRole;
    }
}

module.exports = InterviewRoleDal;