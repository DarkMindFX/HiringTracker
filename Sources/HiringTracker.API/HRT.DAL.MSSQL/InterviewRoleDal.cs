

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
    class InterviewRoleDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IInterviewRoleDal))]
    public class InterviewRoleDal: SQLDal, IInterviewRoleDal
    {
        public IInitParams CreateInitParams()
        {
            return new InterviewRoleDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public InterviewRole Get(System.Int64 InterviewID,System.Int64 UserID)
        {
            InterviewRole result = default(InterviewRole);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_InterviewRole_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@InterviewID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, InterviewID);
            
                            AddParameter(   cmd, "@UserID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, UserID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = InterviewRoleFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64 InterviewID,System.Int64 UserID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_InterviewRole_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@InterviewID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, InterviewID);
            
                            AddParameter(   cmd, "@UserID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, UserID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<InterviewRole> GetByInterviewID(System.Int64 InterviewID)
        {
            var entitiesOut = base.GetBy<InterviewRole, System.Int64>("p_InterviewRole_GetByInterviewID", InterviewID, "@InterviewID", SqlDbType.BigInt, 0, InterviewRoleFromRow);

            return entitiesOut;
        }
                public IList<InterviewRole> GetByUserID(System.Int64 UserID)
        {
            var entitiesOut = base.GetBy<InterviewRole, System.Int64>("p_InterviewRole_GetByUserID", UserID, "@UserID", SqlDbType.BigInt, 0, InterviewRoleFromRow);

            return entitiesOut;
        }
                public IList<InterviewRole> GetByRoleID(System.Int64 RoleID)
        {
            var entitiesOut = base.GetBy<InterviewRole, System.Int64>("p_InterviewRole_GetByRoleID", RoleID, "@RoleID", SqlDbType.BigInt, 0, InterviewRoleFromRow);

            return entitiesOut;
        }
        
        public IList<InterviewRole> GetAll()
        {
            IList<InterviewRole> result = base.GetAll<InterviewRole>("p_InterviewRole_GetAll", InterviewRoleFromRow);

            return result;
        }

        public InterviewRole Upsert(InterviewRole entity) 
        {
            InterviewRole entityOut = base.Upsert<InterviewRole>("p_InterviewRole_Upsert", entity, AddUpsertParameters, InterviewRoleFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, InterviewRole entity)
        {
                SqlParameter pInterviewID = new SqlParameter("@InterviewID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "InterviewID", DataRowVersion.Current, (object)entity.InterviewID != null ? (object)entity.InterviewID : DBNull.Value);   cmd.Parameters.Add(pInterviewID); 
                SqlParameter pUserID = new SqlParameter("@UserID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "UserID", DataRowVersion.Current, (object)entity.UserID != null ? (object)entity.UserID : DBNull.Value);   cmd.Parameters.Add(pUserID); 
                SqlParameter pRoleID = new SqlParameter("@RoleID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "RoleID", DataRowVersion.Current, (object)entity.RoleID != null ? (object)entity.RoleID : DBNull.Value);   cmd.Parameters.Add(pRoleID); 
        
            return cmd;
        }

        protected InterviewRole InterviewRoleFromRow(DataRow row)
        {
            var entity = new InterviewRole();

                    entity.InterviewID = (System.Int64)row["InterviewID"];
                    entity.UserID = (System.Int64)row["UserID"];
                    entity.RoleID = (System.Int64)row["RoleID"];
        
            return entity;
        }
        
    }
}
