
const { SQLDal } = require('./SQLDal');
const UserEntity = require('./entities/User');
const mssql = require('mssql');

class UserDal extends SQLDal {

    init(initParams) {
        super.initDal(initParams);        
    }

    async Upsert(user, editorId) {

        await mssql.connect(this._config);
        let inParams = [];
        inParams['UserID']  = { type: mssql.BigInt, value: user.UserID }; 
        inParams['Login']  = { type: mssql.NVarChar(255), value: user.Login }; 
        inParams['FirstName']  = { type: mssql.NVarChar(255), value: user.FirstName }; 
        inParams['LastName']  = { type: mssql.NVarChar(255), value: user.LastName }; 
        inParams['Email']  = { type: mssql.NVarChar(255), value: user.Email }; 
        inParams['Description']  = { type: mssql.NVarChar(255), value: user.Description };
        inParams['PwdHash']  = { type: mssql.NVarChar(255), value: user.PwdHash };
        inParams['Salt']  = { type: mssql.NVarChar(255), value: user.Salt };
        inParams['ChangedByUserID']  = { type: mssql.NVarChar(255), value: editorId };

        let outParams = [];
        outParams['NewUserID']  = { type: mssql.BigInt, value: null }; 
        
        let result = await super.execStorProcValue(mssql, 'p_User_Upsert', inParams, outParams);

        return result;
    }

    async Delete(UserId) {
        await mssql.connect(this._config);

        let inParams = {};
        inParams['UserID'] = { type: mssql.BigInt, value: UserId };

        let outParams = {};
        outParams['Removed'] = { type: mssql.Bit, value: null }

        let removed = await super.execStorProcValue(mssql, 'p_User_Delete', inParams, outParams);

        return removed;
    }

    async GetAll() {
        let users = null;        

        await mssql.connect(this._config);

        let records = await super.execStorProcRecordset(mssql, 'p_User_GetAll');

        if(records) {
            users = [];
            for(let r in records) {
                let User = this._recordToEntity(records[r]); 
                users.push(User);
            }
        }

        return users;
    }

    async GetDetails(userId) {

        let user = null;        

        await mssql.connect(this._config);

        let inParams = {};
        inParams['UserID'] = { type: mssql.BigInt, value: userId };

        let outParams = {};
        outParams['Found'] = { type: mssql.Bit, value: null };

        let records = await super.execStorProcRecordset(mssql, 'p_User_GetDetails', inParams, outParams);

        if(records) {            
            user = this._recordToEntity(records[0]); 
        }

        return user;
    }

    async GetDetailsByLogin(login) {

        let user = null;        

        await mssql.connect(this._config);

        let inParams = {};
        inParams['Login'] = { type: mssql.NVarChar(50), value: login };

        let outParams = {};
        outParams['Found'] = { type: mssql.Bit, value: null };

        let records = await super.execStorProcRecordset(mssql, 'p_User_GetDetailsByLogin', inParams, outParams);

        if(records) {            
            user = this._recordToEntity(records[0]); 
        }

        return user;
    }

    _recordToEntity(record) {
        let entity = new UserEntity();

        entity.UserID = parseInt(record['UserID']);
        entity.Login = record['Login'];
        entity.FirstName = record['FirstName'];
        entity.LastName = record['LastName'];
        entity.Email = record['Email'];
        entity.Description = record['Description'];
        entity.PwdHash = record['PwdHash'];
        entity.Salt = record['Salt'];

        return entity;
    }
}

module.exports = UserDal;