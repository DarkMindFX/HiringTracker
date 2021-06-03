using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using HRT.Common;
using {DalImplNamespace};
using {DalImplNamespace}.Entities;

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

        public bool Delete(long id)
        {
            bool removed = base.Delete<{Entity}>("p_{Entity}_Delete", id, "{PK_PARAM}");

            return removed;
        }

        public {Entity} Get(long id)
        {
            {Entity} entity = base.Get<{Entity}>("p_{Entity}_GetDetails", id, "{PK_PARAM}", {Entity}FromRow);

            return entity;
        }

        public IList<Candidate> GetAll()
        {
            IList<{Entity}> result = base.GetAll<{Entity}>("p_{Entity}_GetAll", {Entity}FromRow);

            return result;
        }

        public {Entity} Upsert({Entity} entity, long? editorID) 
        { 
        }

        protected {Entity}FromRow(DataRow row)
        {
            var entity = new {Entity}();

    return entity;
        }
    }
}
