

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

        public InterviewStatus Get(System.Int64? ID)
        {
            InterviewStatus result = default(InterviewStatus);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_InterviewStatus_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = InterviewStatusFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_InterviewStatus_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

        
        public IList<InterviewStatus> GetAll()
        {
            IList<InterviewStatus> result = base.GetAll<InterviewStatus>("p_InterviewStatus_GetAll", InterviewStatusFromRow);

            return result;
        }

        public InterviewStatus Insert(InterviewStatus entity) 
        {
            InterviewStatus entityOut = base.Upsert<InterviewStatus>("p_InterviewStatus_Insert", entity, AddUpsertParameters, InterviewStatusFromRow);

            return entityOut;
        }

        public InterviewStatus Update(InterviewStatus entity) 
        {
            InterviewStatus entityOut = base.Upsert<InterviewStatus>("p_InterviewStatus_Update", entity, AddUpsertParameters, InterviewStatusFromRow);

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

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.Name = !DBNull.Value.Equals(row["Name"]) ? (System.String)row["Name"] : default(System.String);
        
            return entity;
        }
        
    }
}
