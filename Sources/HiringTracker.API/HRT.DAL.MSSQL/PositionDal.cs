using HRT.Common;
using HRT.Interfaces;
using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HRT.DAL.MSSQL
{
    class PositionDalInitParams : InitParamsImpl
    {
    }

    public class PositionDal : SQLDal, IPositionDal
    {
        public IInitParams CreateInitParams()
        {
            return new PositionDalInitParams();
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<Position>("p_Position_Delete", id, "@PositionID");

            return removed;
        }

        public Position Get(long id)
        {
            Position entity = base.Get<Position>("p_Position_GetDetails", id, "@PositionID", PositionFromRow);

            return entity;
        }

        public IList<Position> GetAll()
        {
            IList<Position> result = base.GetAll<Position>("p_Position_GetAll", PositionFromRow);

            return result;
        }

        public IList<PositionSkill> GetSkills(long id)
        {
            throw new NotImplementedException();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public void SetSkills(long id, IList<PositionSkill> skills)
        {
            throw new NotImplementedException();
        }

        public long? Upsert(Position entity, long? editorId)
        {
            long? result = null;
            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Position_Upsert", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@PositionID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, entity.PositionID);

                AddParameter(cmd, "@DepartmentID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.DepartmentID));

                AddParameter(cmd, "@StatusID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, entity.StatusID);

                AddParameter(cmd, "@Title", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, entity.Title);

                AddParameter(cmd, "@ShortDesc", SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, entity.ShortDesc);

                AddParameter(cmd, "@Description", SqlDbType.NVarChar, 4000, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, entity.Description);

                AddParameter(cmd, "@ChangedByUserID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, editorId);

                var pNewPosId = AddParameter(cmd, "@NewPositionID", SqlDbType.BigInt, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = !DBNull.Value.Equals(pNewPosId.Value) ? (long?)pNewPosId.Value : null;
            }

            return result;
        }

        #region Support methods
        private Position PositionFromRow(DataRow row)
        {
            var entity = new Position();

            entity.Title = (string)row["Title"];
            entity.PositionID = (long)row["PositionID"];
            entity.ShortDesc = (string)row["ShortDesc"];
            entity.Description = (string)row["Description"];
            entity.StatusID = (long)row["StatusID"];
            entity.DepartmentID = !DBNull.Value.Equals(row["DepartmentID"]) ? (long?)row["DepartmentID"] : null;
            entity.CreatedByID = (long)row["CreatedByID"];
            entity.CreatedDate = (DateTime)row["CreatedDate"];
            entity.ModifiedByID = !DBNull.Value.Equals(row["ModifiedByID"]) ? (long?)row["ModifiedByID"] : null;
            entity.ModifiedDate = !DBNull.Value.Equals(row["ModifiedDate"]) ? (DateTime?)row["ModifiedDate"] : null;

            return entity;
        }
        #endregion
    }
}
