

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
    class UserDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IUserDal))]
    public class UserDal: SQLDal, IUserDal
    {
        public IInitParams CreateInitParams()
        {
            return new UserDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<User>("p_User_Delete", id, "@ID");

            return removed;
        }

        public User Get(long id)
        {
            User entityOut = base.Get<User>("p_User_GetDetails", id, "@ID", UserFromRow);

            return entityOut;
        }

        
        public IList<User> GetAll()
        {
            IList<User> result = base.GetAll<User>("p_User_GetAll", UserFromRow);

            return result;
        }

        public User Upsert(User entity) 
        {
            User entityOut = base.Upsert<User>("p_User_Upsert", entity, AddUpsertParameters, UserFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, User entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pLogin = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Login", DataRowVersion.Current, (object)entity.Login != null ? (object)entity.Login : DBNull.Value);   cmd.Parameters.Add(pLogin); 
                SqlParameter pFirstName = new SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "FirstName", DataRowVersion.Current, (object)entity.FirstName != null ? (object)entity.FirstName : DBNull.Value);   cmd.Parameters.Add(pFirstName); 
                SqlParameter pLastName = new SqlParameter("@LastName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "LastName", DataRowVersion.Current, (object)entity.LastName != null ? (object)entity.LastName : DBNull.Value);   cmd.Parameters.Add(pLastName); 
                SqlParameter pEmail = new SqlParameter("@Email", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Email", DataRowVersion.Current, (object)entity.Email != null ? (object)entity.Email : DBNull.Value);   cmd.Parameters.Add(pEmail); 
                SqlParameter pDescription = new SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 255, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Current, (object)entity.Description != null ? (object)entity.Description : DBNull.Value);   cmd.Parameters.Add(pDescription); 
                SqlParameter pPwdHash = new SqlParameter("@PwdHash", System.Data.SqlDbType.NVarChar, 255, ParameterDirection.Input, false, 0, 0, "PwdHash", DataRowVersion.Current, (object)entity.PwdHash != null ? (object)entity.PwdHash : DBNull.Value);   cmd.Parameters.Add(pPwdHash); 
                SqlParameter pSalt = new SqlParameter("@Salt", System.Data.SqlDbType.NVarChar, 255, ParameterDirection.Input, false, 0, 0, "Salt", DataRowVersion.Current, (object)entity.Salt != null ? (object)entity.Salt : DBNull.Value);   cmd.Parameters.Add(pSalt); 
        
            return cmd;
        }

        protected User UserFromRow(DataRow row)
        {
            var entity = new User();

                    entity.ID = (System.Int64?)row["ID"];
                    entity.Login = (System.String)row["Login"];
                    entity.FirstName = (System.String)row["FirstName"];
                    entity.LastName = (System.String)row["LastName"];
                    entity.Email = (System.String)row["Email"];
                    entity.Description = (System.String)row["Description"];
                    entity.PwdHash = (System.String)row["PwdHash"];
                    entity.Salt = (System.String)row["Salt"];
        
            return entity;
        }
        
    }
}
