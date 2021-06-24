

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

        public UserRoleSystem Get(System.Int64 UserID,System.Int64 RoleID)
        {
            UserRoleSystem result = default(UserRoleSystem);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_UserRoleSystem_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@UserID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, UserID);
            
                            AddParameter(   cmd, "@RoleID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, RoleID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = UserRoleSystemFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64 UserID,System.Int64 RoleID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_UserRoleSystem_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@UserID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, UserID);
            
                            AddParameter(   cmd, "@RoleID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, RoleID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
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
