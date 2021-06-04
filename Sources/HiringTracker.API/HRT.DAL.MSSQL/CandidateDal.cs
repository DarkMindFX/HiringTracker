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
    class CandidateDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(ICandidateDal))]
    public class CandidateDal : SQLDal, ICandidateDal
    {
        public IInitParams CreateInitParams()
        {
            return new CandidateDalInitParams();
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<Candidate>("p_Candidate_Delete", id, "@CandidateID");

            return removed;
        }

        public Candidate Get(long id)
        {
            Candidate entity = base.Get<Candidate>("p_Candidate_GetDetails", id, "@CandidateID", CandidateFromRow);

            return entity;
        }

        public IList<Candidate> GetAll()
        {
            IList<Candidate> result = base.GetAll<Candidate>("p_Candidate_GetAll", CandidateFromRow);

            return result;
        }

        public IList<CandidateSkill> GetSkills(long id)
        {
            IList<CandidateSkill> result = null;

            using (var conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_CandidateSkills_GetByCandidate", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@CandidateID", SqlDbType.BigInt, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, id);

                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Current, null);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count >= 1)
                {
                    result = new List<CandidateSkill>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var p = CandidateSkillFromRow(row);

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

        public void SetSkills(long id, IList<CandidateSkill> skills)
        {
            using (var conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_CandidateSkills_Upsert", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                var tblSkills = CandidateSkillsToTable(skills);

                AddParameter(cmd, "@CandidateID", SqlDbType.BigInt, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, id);

                AddParameter(cmd, "@Skills", SqlDbType.Structured, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, tblSkills);

                cmd.ExecuteNonQuery();
            }
        }

        public long? Upsert(Candidate entity, long? editorID)
        {
            long? result = null;
            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Candidate_Upsert", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@CandidateID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.CandidateID));

                AddParameter(cmd, "@FirstName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, entity.FirstName);

                AddParameter(cmd, "@MiddleName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.MiddleName));

                AddParameter(cmd, "@LastName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, entity.LastName);

                AddParameter(cmd, "@Email", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, entity.Email);

                AddParameter(cmd, "@Phone", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.Phone));

                AddParameter(cmd, "@CVLink", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, entity.CVLink);

                AddParameter(cmd, "@ChangedByUserID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, editorID);

                var pNewId = AddParameter(cmd, "@NewCandidateID", SqlDbType.BigInt, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = !DBNull.Value.Equals(pNewId.Value) ? (long?)pNewId.Value : null;
            }

            return result;
        }

        public Candidate Upsert(Candidate entity)
        {
            throw new NotImplementedException();
        }

        #region Support methods
        private Candidate CandidateFromRow(DataRow row)
        {
            var entity = new Candidate();

            entity.FirstName = (string)row["FirstName"];
            entity.MiddleName = !DBNull.Value.Equals(row["MiddleName"]) ?  (string)row["MiddleName"] : null;
            entity.LastName = (string)row["LastName"];
            entity.CandidateID = (long)row["ID"];
            entity.Email = (string)row["Email"];
            entity.Phone = !DBNull.Value.Equals(row["Phone"]) ? (string)row["Phone"] : null;
            entity.CVLink = (string)row["CVLink"];            
            entity.CreatedByID = (long)row["CreatedByID"];
            entity.CreatedDate = (DateTime)row["CreatedDate"];
            entity.ModifiedByID = !DBNull.Value.Equals(row["ModifiedByID"]) ? (long?)row["ModifiedByID"] : null;
            entity.ModifiedDate = !DBNull.Value.Equals(row["ModifiedDate"]) ? (DateTime?)row["ModifiedDate"] : null;

            return entity;
        }

        private CandidateSkill CandidateSkillFromRow(DataRow row)
        {
            var entity = new CandidateSkill();
            entity.ProficiencyID = (long)row["SkillProficiencyID"];
            entity.ProficiencyName = (string)row["SkillProficiency"];
            entity.SkillName = (string)row["SkillName"];
            entity.SkillID = (long)row["SkillID"];
            entity.CandidateID = (long)row["CandidateID"];

            return entity;
        }

        private DataTable CandidateSkillsToTable(IList<CandidateSkill> skills)
        {
            var table = new DataTable();
            table.Columns.Add(new DataColumn("SkillID", typeof(long)));
            table.Columns.Add(new DataColumn("IsMandatory", typeof(bool)));
            table.Columns.Add(new DataColumn("ProficiencyID", typeof(long)));

            foreach (var s in skills)
            {
                var row = table.NewRow();
                row[0] = s.SkillID;
                row[1] = false;
                row[2] = s.ProficiencyID;

                table.Rows.Add(row);
            }

            return table;
        }

        #endregion
    }
}
