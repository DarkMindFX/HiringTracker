

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

        public bool Delete(long id)
        {
            bool removed = base.Delete<UserRoleCandidate>("p_UserRoleCandidate_Delete", id, "@ID");

            return removed;
        }

        public UserRoleCandidate Get(long id)
        {
            UserRoleCandidate entityOut = base.Get<UserRoleCandidate>("p_UserRoleCandidate_GetDetails", id, "@ID", UserRoleCandidateFromRow);

            return entityOut;
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
