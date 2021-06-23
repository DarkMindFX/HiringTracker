

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

        public bool Delete(long id)
        {
            bool removed = base.Delete<UserRolePosition>("p_UserRolePosition_Delete", id, "@ID");

            return removed;
        }

        public UserRolePosition Get(long id)
        {
            UserRolePosition entityOut = base.Get<UserRolePosition>("p_UserRolePosition_GetDetails", id, "@ID", UserRolePositionFromRow);

            return entityOut;
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

        public UserRolePosition Upsert(UserRolePosition entity) 
        {
            UserRolePosition entityOut = base.Upsert<UserRolePosition>("p_UserRolePosition_Upsert", entity, AddUpsertParameters, UserRolePositionFromRow);

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

                    entity.PositionID = (System.Int64)row["PositionID"];
                    entity.UserID = (System.Int64)row["UserID"];
                    entity.RoleID = (System.Int64)row["RoleID"];
        
            return entity;
        }
        
    }
}
