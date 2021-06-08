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

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<Candidate>("p_Candidate_Delete", id, "@ID");

            return removed;
        }

        public Candidate Get(long id)
        {
            Candidate entityOut = base.Get<Candidate>("p_Candidate_GetDetails", id, "@ID", CandidateFromRow);

            return entityOut;
        }

        public IList<Candidate> GetAll()
        {
            IList<Candidate> result = base.GetAll<Candidate>("p_Candidate_GetAll", CandidateFromRow);

            return result;
        }

        public Candidate Upsert(Candidate entity)
        {
            Candidate entityOut = base.Upsert<Candidate>("p_Candidate_Upsert", entity, AddUpsertParameters, CandidateFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Candidate entity)
        {
            SqlParameter pID = new SqlParameter(@"ID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value); cmd.Parameters.Add(pID);

            SqlParameter pFirstName = new SqlParameter(@"FirstName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "FirstName", DataRowVersion.Current, (object)entity.FirstName != null ? (object)entity.FirstName : DBNull.Value); cmd.Parameters.Add(pFirstName);

            SqlParameter pMiddleName = new SqlParameter(@"MiddleName", SqlDbType.NVarChar, 50, ParameterDirection.Input, true, 0, 0, "MiddleName", DataRowVersion.Current, (object)entity.MiddleName != null ? (object)entity.MiddleName : DBNull.Value); cmd.Parameters.Add(pMiddleName);

            SqlParameter pLastName = new SqlParameter(@"LastName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "LastName", DataRowVersion.Current, (object)entity.LastName != null ? (object)entity.LastName : DBNull.Value); cmd.Parameters.Add(pLastName);

            SqlParameter pEmail = new SqlParameter(@"Email", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Email", DataRowVersion.Current, (object)entity.Email != null ? (object)entity.Email : DBNull.Value); cmd.Parameters.Add(pEmail);

            SqlParameter pPhone = new SqlParameter(@"Phone", SqlDbType.NVarChar, 50, ParameterDirection.Input, true, 0, 0, "Phone", DataRowVersion.Current, (object)entity.Phone != null ? (object)entity.Phone : DBNull.Value); cmd.Parameters.Add(pPhone);

            SqlParameter pCVLink = new SqlParameter(@"CVLink", SqlDbType.NVarChar, 1000, ParameterDirection.Input, true, 0, 0, "CVLink", DataRowVersion.Current, (object)entity.CVLink != null ? (object)entity.CVLink : DBNull.Value); cmd.Parameters.Add(pCVLink);

            SqlParameter pCreatedByID = new SqlParameter(@"CreatedByID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CreatedByID", DataRowVersion.Current, (object)entity.CreatedByID != null ? (object)entity.CreatedByID : DBNull.Value); cmd.Parameters.Add(pCreatedByID);

            SqlParameter pCreatedDate = new SqlParameter(@"CreatedDate", SqlDbType.DateTime2, 0, ParameterDirection.Input, false, 0, 0, "CreatedDate", DataRowVersion.Current, (object)entity.CreatedDate != null ? (object)entity.CreatedDate : DBNull.Value); cmd.Parameters.Add(pCreatedDate);

            SqlParameter pModifiedByID = new SqlParameter(@"ModifiedByID", SqlDbType.BigInt, 0, ParameterDirection.Input, true, 0, 0, "ModifiedByID", DataRowVersion.Current, (object)entity.ModifiedByID != null ? (object)entity.ModifiedByID : DBNull.Value); cmd.Parameters.Add(pModifiedByID);

            SqlParameter pModifiedDate = new SqlParameter(@"ModifiedDate", SqlDbType.DateTime2, 0, ParameterDirection.Input, true, 0, 0, "ModifiedDate", DataRowVersion.Current, (object)entity.ModifiedDate != null ? (object)entity.ModifiedDate : DBNull.Value); cmd.Parameters.Add(pModifiedDate);



            return cmd;
        }

        public IList<CandidateSkill> GetSkills(long id)
        {
            IList<CandidateSkill> result = null;

            using (var conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_CandidateSkill_GetByCandidate", conn);
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

        public void SetSkills(long id, IList<CandidateSkill> skills)
        {
            using (var conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_CandidateSkill_Upsert", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                var tblSkills = CandidateSkillsToTable(skills);

                AddParameter(cmd, "@CandidateID", SqlDbType.BigInt, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, id);

                AddParameter(cmd, "@Skills", SqlDbType.Structured, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, tblSkills);

                cmd.ExecuteNonQuery();
            }
        }

        protected Candidate CandidateFromRow(DataRow row)
        {
            var entity = new Candidate();

            entity.ID = (System.Int64?)row["ID"];
            entity.FirstName = (System.String)row["FirstName"];
            entity.MiddleName = !DBNull.Value.Equals(row["MiddleName"]) ? (System.String)row["MiddleName"] : null;
            entity.LastName = (System.String)row["LastName"];
            entity.Email = (System.String)row["Email"];
            entity.Phone = !DBNull.Value.Equals(row["Phone"]) ? (System.String)row["Phone"] : null;
            entity.CVLink = !DBNull.Value.Equals(row["CVLink"]) ? (System.String)row["CVLink"] : null;
            entity.CreatedByID = (System.Int64)row["CreatedByID"];
            entity.CreatedDate = (System.DateTime)row["CreatedDate"];
            entity.ModifiedByID = !DBNull.Value.Equals(row["ModifiedByID"]) ? (System.Int64?)row["ModifiedByID"] : null;
            entity.ModifiedDate = !DBNull.Value.Equals(row["ModifiedDate"]) ? (System.DateTime?)row["ModifiedDate"] : null;


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

        public long? Upsert(Candidate entity, long? editorID)
        {
            throw new NotImplementedException();
        }
    }
}
