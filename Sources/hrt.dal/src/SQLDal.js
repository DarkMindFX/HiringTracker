

class SQLDal {

    initDal(initParams) {
        this._initParams = initParams;
        this._config = {
            user: initParams['username'],
            password: initParams['password'],
            server: initParams['server'],
            database: initParams['database']        
        }
    }

    async execStorProcRecordset(sql, name, inParams, outParams) {

        let request = new sql.Request();

        if(inParams) {
            for(var inp in inParams) {
                request.input(inp, inParams[inp].type, inParams[inp].value);
            }
        }

        if(outParams) {
            for(var outp in outParams) {
                request.output(outp, outParams[outp].type);
            }
        }


        let response = await request.execute(name);

        return response.recordset;
    }

    async execStorProcValue(sql, name, inParams, outParams) {

        let request = new sql.Request();

        for(var inp in inParams) {
            request.input(inp, inParams[inp].type, inParams[inp].value);
        }

        for(var outp in outParams) {
            request.output(outp, outParams[outp].type);
        }

        let response = await request.execute(name);

        return response.output;
    }
}

module.exports = {
    SQLDal    
}