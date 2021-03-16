
const { SQLDal } = require('./SQLDal');
const InterviewTypeEntity = require('./entities/InterviewType');
const mssql = require('mssql');

class InterviewTypeDal extends SQLDal {

    init(initParams) {
        super.initDal(initParams);        
    }

    async GetAll() {
        let types = null;
        await mssql.connect(this._config);

        let records = await super.execStorProcRecordset(mssql, 'p_InterviewType_GetAll');

        if(records) {
            types = [];
            for(let r in records) {
                let type = new InterviewTypeEntity();
                type.TypeID = parseInt(records[r].TypeID);
                type.Name = records[r].Name;

                types.push(type);
            }
        }

        return types;
    }
}

module.exports = InterviewTypeDal;