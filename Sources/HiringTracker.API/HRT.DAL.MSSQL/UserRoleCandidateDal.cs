

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
    class UserRoleCandidateDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IUserRoleCandidateDal))]
    public class UserRoleCandidateDal: SQLDal, IUserRoleCandidateDal
    {
        public IInitParams CreateInitParams()
        {
            return new UserRoleCandidateDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        UserRoleCandidate Get(System.Int64 CandidateID,System.Int64 UserID)
        {
            UserRoleCandidate result = default(UserRoleCandidate);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_UserRoleCandidate_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@CandidateID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, CandidateID);
            
                            AddParameter(   cmd, "@UserID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, UserID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = UserRoleCandidateFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        bool Delete(System.Int64 CandidateID,System.Int64 UserID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_UserRoleCandidate_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@CandidateID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, CandidateID);
            
                            AddParameter(   cmd, "@UserID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, UserID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<UserRoleCandidate> GetByCandidateID(System.Int64 CandidateID)
        {
            var entitiesOut = base.GetBy<UserRoleCandidate, System.Int64>("p_UserRoleCandidate_GetByCandidateID", CandidateID, "@CandidateID", SqlDbType.BigInt, 0, UserRoleCandidateFromRow);

            return entitiesOut;
        }
                public IList<UserRoleCandidate> GetByUserID(System.Int64 UserID)
        {
            var entitiesOut = base.GetBy<UserRoleCandidate, System.Int64>("p_UserRoleCandidate_GetByUserID", UserID, "@UserID", SqlDbType.BigInt, 0, UserRoleCandidateFromRow);

            return entitiesOut;
        }
                public IList<UserRoleCandidate> GetByRoleID(System.Int64 RoleID)
        {
            var entitiesOut = base.GetBy<UserRoleCandidate, System.Int64>("p_UserRoleCandidate_GetByRoleID", RoleID, "@RoleID", SqlDbType.BigInt, 0, UserRoleCandidateFromRow);

            return entitiesOut;
        }
        
        public IList<UserRoleCandidate> GetAll()
        {
            IList<UserRoleCandidate> result = base.GetAll<UserRoleCandidate>("p_UserRoleCandidate_GetAll", UserRoleCandidateFromRow);

            return result;
        }

        public UserRoleCandidate Upsert(UserRoleCandidate entity) 
        {
            UserRoleCandidate entityOut = base.Upsert<UserRoleCandidate>("p_UserRoleCandidate_Upsert", entity, AddUpsertParameters, UserRoleCandidateFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, UserRoleCandidate entity)
        {
                SqlParameter pCandidateID = new SqlParameter("@CandidateID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CandidateID", DataRowVersion.Current, (object)entity.CandidateID != null ? (object)entity.CandidateID : DBNull.Value);   cmd.Parameters.Add(pCandidateID); 
                SqlParameter pUserID = new SqlParameter("@UserID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "UserID", DataRowVersion.Current, (object)entity.UserID != null ? (object)entity.UserID : DBNull.Value);   cmd.Parameters.Add(pUserID); 
                SqlParameter pRoleID = new SqlParameter("@RoleID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "RoleID", DataRowVersion.Current, (object)entity.RoleID != null ? (object)entity.RoleID : DBNull.Value);   cmd.Parameters.Add(pRoleID); 
        
            return cmd;
        }

        protected UserRoleCandidate UserRoleCandidateFromRow(DataRow row)
        {
            var entity = new UserRoleCandidate();

                    entity.CandidateID = (System.Int64)row["CandidateID"];
                    entity.UserID = (System.Int64)row["UserID"];
                    entity.RoleID = (System.Int64)row["RoleID"];
        
            return entity;
        }
        
    }
}
