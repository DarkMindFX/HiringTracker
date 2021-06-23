

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using HRT.Common;
using HRT.DAL.MSSQL;
using HRT.Interfaces;
using HRT.Interfaces.Entities;

namespace HRT.DAL.MSSQL 
{
    class InterviewTypeDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IInterviewTypeDal))]
    public class InterviewTypeDal: SQLDal, IInterviewTypeDal
    {
        public IInitParams CreateInitParams()
        {
            return new InterviewTypeDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<InterviewType>("p_InterviewType_Delete", id, "@ID");

            return removed;
        }

        public InterviewType Get(long id)
        {
            InterviewType entityOut = base.Get<InterviewType>("p_InterviewType_GetDetails", id, "@ID", InterviewTypeFromRow);

            return entityOut;
        }

        
        public IList<InterviewType> GetAll()
        {
            IList<InterviewType> result = base.GetAll<InterviewType>("p_InterviewType_GetAll", InterviewTypeFromRow);

            return result;
        }

        public InterviewType Upsert(InterviewType entity) 
        {
            InterviewType entityOut = base.Upsert<InterviewType>("p_InterviewType_Upsert", entity, AddUpsertParameters, InterviewTypeFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, InterviewType entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pName = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, (object)entity.Name != null ? (object)entity.Name : DBNull.Value);   cmd.Parameters.Add(pName); 
        
            return cmd;
        }

        protected InterviewType InterviewTypeFromRow(DataRow row)
        {
            var entity = new InterviewType();

                    entity.ID = (System.Int64)row["ID"];
                    entity.Name = (System.String)row["Name"];
        
            return entity;
        }
        
    }
}
