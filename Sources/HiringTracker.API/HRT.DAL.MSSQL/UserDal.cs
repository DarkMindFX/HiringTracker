

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
    public class UserDal : SQLDal, IUserDal
    {
        public IInitParams CreateInitParams()
        {
            return new UserDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public User Get(System.Int64? ID)
        {
            User result = default(User);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_User_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                               ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);


                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = UserFromRow(ds.Tables[0].Rows[0]);
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_User_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);

                var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


        public IList<User> GetAll()
        {
            IList<User> result = base.GetAll<User>("p_User_GetAll", UserFromRow);

            return result;
        }

        public User Insert(User entity)
        {
            User entityOut = base.Upsert<User>("p_User_Insert", entity, AddUpsertParameters, UserFromRow);

            return entityOut;
        }

        public User Update(User entity)
        {
            User entityOut = base.Upsert<User>("p_User_Update", entity, AddUpsertParameters, UserFromRow);

            return entityOut;
        }

        public User GetByLogin(string Login)
        {
            User result = default(User);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_User_GetDetailsByLogin", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@Login", System.Data.SqlDbType.NVarChar, 50,
                               ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, Login);


                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = UserFromRow(ds.Tables[0].Rows[0]);
                }
            }

            return result;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, User entity)
        {
            SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value); cmd.Parameters.Add(pID);
            SqlParameter pLogin = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Login", DataRowVersion.Current, (object)entity.Login != null ? (object)entity.Login : DBNull.Value); cmd.Parameters.Add(pLogin);
            SqlParameter pFirstName = new SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "FirstName", DataRowVersion.Current, (object)entity.FirstName != null ? (object)entity.FirstName : DBNull.Value); cmd.Parameters.Add(pFirstName);
            SqlParameter pLastName = new SqlParameter("@LastName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "LastName", DataRowVersion.Current, (object)entity.LastName != null ? (object)entity.LastName : DBNull.Value); cmd.Parameters.Add(pLastName);
            SqlParameter pEmail = new SqlParameter("@Email", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Email", DataRowVersion.Current, (object)entity.Email != null ? (object)entity.Email : DBNull.Value); cmd.Parameters.Add(pEmail);
            SqlParameter pDescription = new SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 255, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Current, (object)entity.Description != null ? (object)entity.Description : DBNull.Value); cmd.Parameters.Add(pDescription);
            SqlParameter pPwdHash = new SqlParameter("@PwdHash", System.Data.SqlDbType.NVarChar, 255, ParameterDirection.Input, false, 0, 0, "PwdHash", DataRowVersion.Current, (object)entity.PwdHash != null ? (object)entity.PwdHash : DBNull.Value); cmd.Parameters.Add(pPwdHash);
            SqlParameter pSalt = new SqlParameter("@Salt", System.Data.SqlDbType.NVarChar, 255, ParameterDirection.Input, false, 0, 0, "Salt", DataRowVersion.Current, (object)entity.Salt != null ? (object)entity.Salt : DBNull.Value); cmd.Parameters.Add(pSalt);

            return cmd;
        }

        protected User UserFromRow(DataRow row)
        {
            var entity = new User();

            entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
            entity.Login = !DBNull.Value.Equals(row["Login"]) ? (System.String)row["Login"] : default(System.String);
            entity.FirstName = !DBNull.Value.Equals(row["FirstName"]) ? (System.String)row["FirstName"] : default(System.String);
            entity.LastName = !DBNull.Value.Equals(row["LastName"]) ? (System.String)row["LastName"] : default(System.String);
            entity.Email = !DBNull.Value.Equals(row["Email"]) ? (System.String)row["Email"] : default(System.String);
            entity.Description = !DBNull.Value.Equals(row["Description"]) ? (System.String)row["Description"] : default(System.String);
            entity.PwdHash = !DBNull.Value.Equals(row["PwdHash"]) ? (System.String)row["PwdHash"] : default(System.String);
            entity.Salt = !DBNull.Value.Equals(row["Salt"]) ? (System.String)row["Salt"] : default(System.String);

            return entity;
        }

        
    }
}
