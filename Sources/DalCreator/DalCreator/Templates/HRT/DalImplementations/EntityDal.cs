using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using HRT.Common;
using HRT.DAL.MSSQL;
using {DalNamespace};
using {DalNamespace}.Entities;

namespace {DalImplNamespace} 
{
    class {Entity}DalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(I{Entity}Dal))]
    public class {Entity}Dal: SQLDal, I{Entity}Dal
    {
        public IInitParams CreateInitParams()
        {
            return new {Entity}DalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<{Entity}>("p_{Entity}_Delete", id, "@ID");

            return removed;
        }

        public {Entity} Get(long id)
        {
            {Entity} entityOut = base.Get<{Entity}>("p_{Entity}_GetDetails", id, "@ID", {Entity}FromRow);

            return entityOut;
        }

        public IList<{Entity}> GetAll()
        {
            IList<{Entity}> result = base.GetAll<{Entity}>("p_{Entity}_GetAll", {Entity}FromRow);

            return result;
        }

        public {Entity} Upsert({Entity} entity) 
        {
            {Entity} entityOut = base.Upsert<{Entity}>("p_{Entity}_Upsert", entity, AddUpsertParameters, {Entity}FromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, {Entity} entity)
        {
            {UPSERT_PARAMS_LIST}

            return cmd;
        }

        protected {Entity} {Entity}FromRow(DataRow row)
        {
            var entity = new {Entity}();

            {ROW_TO_ENTITY_LIST}

            return entity;
        }
    }
}
