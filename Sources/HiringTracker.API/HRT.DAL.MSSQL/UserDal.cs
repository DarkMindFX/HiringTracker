using HRT.Common;
using HRT.Interfaces;
using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.Text;

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

        public bool Delete(long id)
        {
            bool removed = base.Delete<User>("p_User_Delete", id, "@UserID");

            return removed;
        }

        public User Get(long id)
        {
            User entity = base.Get<User>("p_User_GetDetails", id, "@UserID", UserFromRow);

            return entity;
        }

        public IList<User> GetAll()
        {
            IList<User> result = base.GetAll<User>("p_User_GetAll", UserFromRow);

            return result;
        }

        public User GetByLogin(string login)
        {
            User result = default(User);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_User_GetDetailsByLogin", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@Login", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, login);

                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if ((bool)pFound.Value && ds.Tables.Count >= 1 && ds.Tables[0].Rows.Count > 0)
                {
                    result = UserFromRow(ds.Tables[0].Rows[0]);
                }
            }

            return result;
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public long? Upsert(User entity, long? editorID)
        {
            long? result = null;
            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_User_Upsert", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@UserID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.UserID));

                AddParameter(cmd, "@Login", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.Login));

                AddParameter(cmd, "@Email", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.Email));

                AddParameter(cmd, "@Description", SqlDbType.NVarChar, 255, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.Description));

                AddParameter(cmd, "@FirstName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.FirstName));

                AddParameter(cmd, "@LastName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.LastName));

                AddParameter(cmd, "@PwdHash", SqlDbType.NVarChar, 255, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.PasswordHash));

                AddParameter(cmd, "@Salt", SqlDbType.NVarChar, 255, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.Salt));

                AddParameter(cmd, "@ChangedByUserID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(editorID));

                var pNewUserId = AddParameter(cmd, "@NewUserID", SqlDbType.BigInt, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = !DBNull.Value.Equals(pNewUserId.Value) ? (long?)pNewUserId.Value : null;
            }

            return result;
        }

        #region Support methods
        public User UserFromRow(DataRow row)
        {
            var entity = new User();

            entity.UserID = (long)row["UserID"];
            entity.Login = (string)row["Login"];
            entity.Salt = (string)row["Salt"];
            entity.FirstName = (string)row["FirstName"];
            entity.LastName = (string)row["LastName"];
            entity.Email = (string)row["Email"];
            entity.Description = !DBNull.Value.Equals(row["Description"]) ? (string)row["Description"] : null;
            entity.PasswordHash = (string)row["PwdHash"];

            return entity;
        }
        #endregion
    }
}
