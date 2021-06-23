

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
    class UserRoleSystemDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IUserRoleSystemDal))]
    public class UserRoleSystemDal: SQLDal, IUserRoleSystemDal
    {
        public IInitParams CreateInitParams()
        {
            return new UserRoleSystemDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<UserRoleSystem>("p_UserRoleSystem_Delete", id, "@ID");

            return removed;
        }

        public UserRoleSystem Get(long id)
        {
            UserRoleSystem entityOut = base.Get<UserRoleSystem>("p_UserRoleSystem_GetDetails", id, "@ID", UserRoleSystemFromRow);

            return entityOut;
        }

                public IList<UserRoleSystem> GetByUserID(System.Int64 UserID)
        {
            var entitiesOut = base.GetBy<UserRoleSystem, System.Int64>("p_UserRoleSystem_GetByUserID", UserID, "@UserID", SqlDbType.BigInt, 0, UserRoleSystemFromRow);

            return entitiesOut;
        }
                public IList<UserRoleSystem> GetByRoleID(System.Int64 RoleID)
        {
            var entitiesOut = base.GetBy<UserRoleSystem, System.Int64>("p_UserRoleSystem_GetByRoleID", RoleID, "@RoleID", SqlDbType.BigInt, 0, UserRoleSystemFromRow);

            return entitiesOut;
        }
        
        public IList<UserRoleSystem> GetAll()
        {
            IList<UserRoleSystem> result = base.GetAll<UserRoleSystem>("p_UserRoleSystem_GetAll", UserRoleSystemFromRow);

            return result;
        }

        public UserRoleSystem Upsert(UserRoleSystem entity) 
        {
            UserRoleSystem entityOut = base.Upsert<UserRoleSystem>("p_UserRoleSystem_Upsert", entity, AddUpsertParameters, UserRoleSystemFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, UserRoleSystem entity)
        {
                SqlParameter pUserID = new SqlParameter("@UserID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "UserID", DataRowVersion.Current, (object)entity.UserID != null ? (object)entity.UserID : DBNull.Value);   cmd.Parameters.Add(pUserID); 
                SqlParameter pRoleID = new SqlParameter("@RoleID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "RoleID", DataRowVersion.Current, (object)entity.RoleID != null ? (object)entity.RoleID : DBNull.Value);   cmd.Parameters.Add(pRoleID); 
        
            return cmd;
        }

        protected UserRoleSystem UserRoleSystemFromRow(DataRow row)
        {
            var entity = new UserRoleSystem();

                    entity.UserID = (System.Int64)row["UserID"];
                    entity.RoleID = (System.Int64)row["RoleID"];
        
            return entity;
        }
        
    }
}
