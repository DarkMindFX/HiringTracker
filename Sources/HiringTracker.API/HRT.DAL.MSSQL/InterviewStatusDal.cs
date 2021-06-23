

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
    class InterviewStatusDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IInterviewStatusDal))]
    public class InterviewStatusDal: SQLDal, IInterviewStatusDal
    {
        public IInitParams CreateInitParams()
        {
            return new InterviewStatusDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<InterviewStatus>("p_InterviewStatus_Delete", id, "@ID");

            return removed;
        }

        public InterviewStatus Get(long id)
        {
            InterviewStatus entityOut = base.Get<InterviewStatus>("p_InterviewStatus_GetDetails", id, "@ID", InterviewStatusFromRow);

            return entityOut;
        }

        
        public IList<InterviewStatus> GetAll()
        {
            IList<InterviewStatus> result = base.GetAll<InterviewStatus>("p_InterviewStatus_GetAll", InterviewStatusFromRow);

            return result;
        }

        public InterviewStatus Upsert(InterviewStatus entity) 
        {
            InterviewStatus entityOut = base.Upsert<InterviewStatus>("p_InterviewStatus_Upsert", entity, AddUpsertParameters, InterviewStatusFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, InterviewStatus entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pName = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, (object)entity.Name != null ? (object)entity.Name : DBNull.Value);   cmd.Parameters.Add(pName); 
        
            return cmd;
        }

        protected InterviewStatus InterviewStatusFromRow(DataRow row)
        {
            var entity = new InterviewStatus();

                    entity.ID = (System.Int64?)row["ID"];
                    entity.Name = (System.String)row["Name"];
        
            return entity;
        }
        
    }
}
