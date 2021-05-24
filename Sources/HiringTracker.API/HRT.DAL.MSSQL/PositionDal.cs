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
    class PositionDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IPositionDal))]
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
            IList<PositionSkill> result = null;

            using (var conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_PositionSkills_GetByPosition", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@PositionID", SqlDbType.BigInt, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, id);

                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Current, null);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count >= 1)
                {
                    result = new List<PositionSkill>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var p = PositionSkillFromRow(row);

                        result.Add(p);
                    }
                }
            }

            return result;
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public void SetSkills(long id, IList<PositionSkill> skills)
        {
            using (var conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_PositionSkills_Upsert", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                var tblSkills = PositionSkillsToTable(skills);

                AddParameter(cmd, "@PositionID", SqlDbType.BigInt, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, id);

                AddParameter(cmd, "@Skills", SqlDbType.Structured, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, tblSkills);

                cmd.ExecuteNonQuery();
            }
        }

        public long? Upsert(Position entity, long? editorID)
        {
            long? result = null;
            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Position_Upsert", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@PositionID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.PositionID));

                AddParameter(cmd, "@DepartmentID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.DepartmentID));

                AddParameter(cmd, "@StatusID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, entity.StatusID);

                AddParameter(cmd, "@Title", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, entity.Title);

                AddParameter(cmd, "@ShortDesc", SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, entity.ShortDesc);

                AddParameter(cmd, "@Description", SqlDbType.NVarChar, 4000, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, entity.Description);

                AddParameter(cmd, "@ChangedByUserID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, editorID);

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

        private PositionSkill PositionSkillFromRow(DataRow row)
        {
            var entity = new PositionSkill();
            entity.IsMandatory = (bool)row["IsMandatory"];
            entity.ProficiencyID = (long)row["SkillProficiencyID"];
            entity.ProficiencyName = (string)row["SkillProficiency"];
            entity.SkillName = (string)row["SkillName"];
            entity.SkillID = (long)row["SkillID"];
            entity.PositionID = (long)row["PositionID"];

            return entity;
        }

        private DataTable PositionSkillsToTable(IList<PositionSkill> skills)
        {
            var table = new DataTable();
            table.Columns.Add(new DataColumn("SkillID", typeof(long)));
            table.Columns.Add(new DataColumn("IsMandatory", typeof(bool)));
            table.Columns.Add(new DataColumn("ProficiencyID", typeof(long)));

            foreach(var s in skills)
            {
                var row = table.NewRow();
                row[0] = s.SkillID;
                row[1] = s.IsMandatory;
                row[2] = s.ProficiencyID;

                table.Rows.Add(row);
            }

            return table;
        }

        #endregion
    }
}
