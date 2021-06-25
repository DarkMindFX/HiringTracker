

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
    class UserRolePositionDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IUserRolePositionDal))]
    public class UserRolePositionDal: SQLDal, IUserRolePositionDal
    {
        public IInitParams CreateInitParams()
        {
            return new UserRolePositionDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public UserRolePosition Get(System.Int64 PositionID,System.Int64 UserID)
        {
            UserRolePosition result = default(UserRolePosition);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_UserRolePosition_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@PositionID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, PositionID);
            
                            AddParameter(   cmd, "@UserID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, UserID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = UserRolePositionFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64 PositionID,System.Int64 UserID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_UserRolePosition_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@PositionID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, PositionID);
            
                            AddParameter(   cmd, "@UserID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, UserID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<UserRolePosition> GetByPositionID(System.Int64 PositionID)
        {
            var entitiesOut = base.GetBy<UserRolePosition, System.Int64>("p_UserRolePosition_GetByPositionID", PositionID, "@PositionID", SqlDbType.BigInt, 0, UserRolePositionFromRow);

            return entitiesOut;
        }
                public IList<UserRolePosition> GetByUserID(System.Int64 UserID)
        {
            var entitiesOut = base.GetBy<UserRolePosition, System.Int64>("p_UserRolePosition_GetByUserID", UserID, "@UserID", SqlDbType.BigInt, 0, UserRolePositionFromRow);

            return entitiesOut;
        }
                public IList<UserRolePosition> GetByRoleID(System.Int64 RoleID)
        {
            var entitiesOut = base.GetBy<UserRolePosition, System.Int64>("p_UserRolePosition_GetByRoleID", RoleID, "@RoleID", SqlDbType.BigInt, 0, UserRolePositionFromRow);

            return entitiesOut;
        }
        
        public IList<UserRolePosition> GetAll()
        {
            IList<UserRolePosition> result = base.GetAll<UserRolePosition>("p_UserRolePosition_GetAll", UserRolePositionFromRow);

            return result;
        }

        public UserRolePosition Insert(UserRolePosition entity) 
        {
            UserRolePosition entityOut = base.Upsert<UserRolePosition>("p_UserRolePosition_Insert", entity, AddUpsertParameters, UserRolePositionFromRow);

            return entityOut;
        }

        public UserRolePosition Update(UserRolePosition entity) 
        {
            UserRolePosition entityOut = base.Upsert<UserRolePosition>("p_UserRolePosition_Update", entity, AddUpsertParameters, UserRolePositionFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, UserRolePosition entity)
        {
                SqlParameter pPositionID = new SqlParameter("@PositionID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "PositionID", DataRowVersion.Current, (object)entity.PositionID != null ? (object)entity.PositionID : DBNull.Value);   cmd.Parameters.Add(pPositionID); 
                SqlParameter pUserID = new SqlParameter("@UserID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "UserID", DataRowVersion.Current, (object)entity.UserID != null ? (object)entity.UserID : DBNull.Value);   cmd.Parameters.Add(pUserID); 
                SqlParameter pRoleID = new SqlParameter("@RoleID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "RoleID", DataRowVersion.Current, (object)entity.RoleID != null ? (object)entity.RoleID : DBNull.Value);   cmd.Parameters.Add(pRoleID); 
        
            return cmd;
        }

        protected UserRolePosition UserRolePositionFromRow(DataRow row)
        {
            var entity = new UserRolePosition();

                    entity.PositionID = !DBNull.Value.Equals(row["PositionID"]) ? (System.Int64)row["PositionID"] : default(System.Int64);
                    entity.UserID = !DBNull.Value.Equals(row["UserID"]) ? (System.Int64)row["UserID"] : default(System.Int64);
                    entity.RoleID = !DBNull.Value.Equals(row["RoleID"]) ? (System.Int64)row["RoleID"] : default(System.Int64);
        
            return entity;
        }
        
    }
}
