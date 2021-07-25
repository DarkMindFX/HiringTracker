

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
    class CandidateSkillDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(ICandidateSkillDal))]
    public class CandidateSkillDal : SQLDal, ICandidateSkillDal
    {
        public IInitParams CreateInitParams()
        {
            return new CandidateSkillDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public CandidateSkill Get(System.Int64 CandidateID, System.Int64 SkillID)
        {
            CandidateSkill result = default(CandidateSkill);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_CandidateSkill_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@CandidateID", System.Data.SqlDbType.BigInt, 0,
                               ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, CandidateID);

                AddParameter(cmd, "@SkillID", System.Data.SqlDbType.BigInt, 0,
                    ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, SkillID);


                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = CandidateSkillFromRow(ds.Tables[0].Rows[0]);
                }
            }

            return result;
        }

        public void SetCandidateSkills(long CandidateID, IList<CandidateSkill> Skills)
        {
            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_CandidateSkill_Upsert", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@CandidateID", System.Data.SqlDbType.BigInt, 0,
                               ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, CandidateID);

                var skills = CandidateSkillsToTable(Skills);
                var pSkills = AddParameter(cmd, "@Skills", System.Data.SqlDbType.Structured, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, skills);
                pSkills.TypeName = "dbo.TYPE_Candidate_Skills";

                cmd.ExecuteNonQuery();
            }
        }

        public bool Delete(System.Int64 CandidateID, System.Int64 SkillID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_CandidateSkill_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@CandidateID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, CandidateID);

                AddParameter(cmd, "@SkillID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, SkillID);

                var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

        public IList<CandidateSkill> GetByCandidateID(System.Int64 CandidateID)
        {
            var entitiesOut = base.GetBy<CandidateSkill, System.Int64>("p_CandidateSkill_GetByCandidateID", CandidateID, "@CandidateID", SqlDbType.BigInt, 0, CandidateSkillFromRow);

            return entitiesOut;
        }
        public IList<CandidateSkill> GetBySkillID(System.Int64 SkillID)
        {
            var entitiesOut = base.GetBy<CandidateSkill, System.Int64>("p_CandidateSkill_GetBySkillID", SkillID, "@SkillID", SqlDbType.BigInt, 0, CandidateSkillFromRow);

            return entitiesOut;
        }
        public IList<CandidateSkill> GetBySkillProficiencyID(System.Int64 SkillProficiencyID)
        {
            var entitiesOut = base.GetBy<CandidateSkill, System.Int64>("p_CandidateSkill_GetBySkillProficiencyID", SkillProficiencyID, "@SkillProficiencyID", SqlDbType.BigInt, 0, CandidateSkillFromRow);

            return entitiesOut;
        }

        public IList<CandidateSkill> GetAll()
        {
            IList<CandidateSkill> result = base.GetAll<CandidateSkill>("p_CandidateSkill_GetAll", CandidateSkillFromRow);

            return result;
        }

        public CandidateSkill Insert(CandidateSkill entity)
        {
            CandidateSkill entityOut = base.Upsert<CandidateSkill>("p_CandidateSkill_Insert", entity, AddUpsertParameters, CandidateSkillFromRow);

            return entityOut;
        }

        public CandidateSkill Update(CandidateSkill entity)
        {
            CandidateSkill entityOut = base.Upsert<CandidateSkill>("p_CandidateSkill_Update", entity, AddUpsertParameters, CandidateSkillFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, CandidateSkill entity)
        {
            SqlParameter pCandidateID = new SqlParameter("@CandidateID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CandidateID", DataRowVersion.Current, (object)entity.CandidateID != null ? (object)entity.CandidateID : DBNull.Value); cmd.Parameters.Add(pCandidateID);
            SqlParameter pSkillID = new SqlParameter("@SkillID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "SkillID", DataRowVersion.Current, (object)entity.SkillID != null ? (object)entity.SkillID : DBNull.Value); cmd.Parameters.Add(pSkillID);
            SqlParameter pSkillProficiencyID = new SqlParameter("@SkillProficiencyID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "SkillProficiencyID", DataRowVersion.Current, (object)entity.SkillProficiencyID != null ? (object)entity.SkillProficiencyID : DBNull.Value); cmd.Parameters.Add(pSkillProficiencyID);

            return cmd;
        }

        protected CandidateSkill CandidateSkillFromRow(DataRow row)
        {
            var entity = new CandidateSkill();

            entity.CandidateID = !DBNull.Value.Equals(row["CandidateID"]) ? (System.Int64)row["CandidateID"] : default(System.Int64);
            entity.SkillID = !DBNull.Value.Equals(row["SkillID"]) ? (System.Int64)row["SkillID"] : default(System.Int64);
            entity.SkillProficiencyID = !DBNull.Value.Equals(row["SkillProficiencyID"]) ? (System.Int64)row["SkillProficiencyID"] : default(System.Int64);

            return entity;
        }

        protected DataTable CandidateSkillsToTable(IList<CandidateSkill> candidateSkills)
        {
            var table = new DataTable();
            table.Columns.Add("SkillID", typeof(long));
            table.Columns.Add("IsMandatory", typeof(bool));
            table.Columns.Add("ProficiencyID", typeof(long));

            foreach (var ps in candidateSkills)
            {
                var r = table.NewRow();
                r["SkillID"] = ps.SkillID;
                r["ProficiencyID"] = ps.SkillProficiencyID;
                r["IsMandatory"] = false;

                table.Rows.Add(r);
            }

            return table;
        }

    }
}
