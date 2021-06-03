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
    class PositionCandidateDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IPositionCandidateDal))]
    public class PositionCandidateDal : SQLDal, IPositionCandidateDal
    {
        public IInitParams CreateInitParams()
        {
            return new PositionCandidateDalInitParams();
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<Candidate>("p_PositionCandidates_Delete", id, "@ProposalID");

            return removed;
        }

        public PositionCandidate Get(long id)
        {
            PositionCandidate entity = base.Get<PositionCandidate>("p_PositionCandidates_GetDetails", id, "@ProposalID", PositionCandidateFromRow);

            return entity;
        }

        public IList<PositionCandidate> GetAll()
        {
            IList<PositionCandidate> result = base.GetAll<PositionCandidate>("p_PositionCandidates_GetAll", PositionCandidateFromRow);

            return result;
        }

        public IList<PositionCandidate> GetByCandidate(long id)
        {
            IList<PositionCandidate> result = new List<PositionCandidate>();

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_PositionCandidate_GetByCandidate", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@CandidateID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, id);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count >= 1)
                {
                    result = new List<PositionCandidate>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var c = PositionCandidateFromRow(row);

                        result.Add(c);
                    }
                }
            }

            return result;
        }

        public IList<PositionCandidate> GetByPosition(long id)
        {
            IList<PositionCandidate> result = new List<PositionCandidate>();

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_PositionCandidate_GetByPosition", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@PositionID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, id);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count >= 1)
                {
                    result = new List<PositionCandidate>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var c = PositionCandidateFromRow(row);

                        result.Add(c);
                    }
                }
            }

            return result;
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public long? Upsert(PositionCandidate entity, long? editorID)
        {
            long? result = null;
            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_PositionCandidate_Upsert", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@ProposalID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.PositionID));

                AddParameter(cmd, "@PositionID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.PositionID));

                AddParameter(cmd, "@CandidateID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.CandidateID));

                AddParameter(cmd, "@Proposed", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.Proposed));

                AddParameter(cmd, "@CurrentStepID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.CurrentStepID));

                AddParameter(cmd, "@NextStepID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.NextStepID));

                AddParameter(cmd, "@DueDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.DueDate));

                AddParameter(cmd, "@StatusID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ValueOrDBNull(entity.StatusID));

                var pNewId = AddParameter(cmd, "@NewProposalID", SqlDbType.BigInt, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, null);

                cmd.ExecuteNonQuery();

                result = !DBNull.Value.Equals(pNewId.Value) ? (long?)pNewId.Value : null;
            }

            return result;
        }

        public PositionCandidate Upsert(PositionCandidate entity)
        {
            throw new NotImplementedException();
        }

        #region Support methods
        private PositionCandidate PositionCandidateFromRow(DataRow row)
        {
            var entity = new PositionCandidate();
            entity.ProposalID = (long)row["ProposalID"];
            entity.PositionID = (long)row["PositionID"];
            entity.CandidateID = (long)row["CandidateID"];
            entity.CurrentStepID = (long)row["CurrentStepID"];
            entity.CurrentStepName = (string)row["CurrentStepName"];
            entity.NextStepID = (long)row["NextStepID"];
            entity.NextStepName = (string)row["NextStepName"];
            entity.StatusID = (long)row["StatusID"];
            entity.StatusName = (string)row["StatusName"];
            entity.Proposed = (DateTime)row["Proposed"];
            entity.DueDate = !DBNull.Value.Equals(row["DueDate"]) ? (DateTime?)row["DueDate"] : null;

            entity.CreatedByID = (long)row["CreatedByID"];
            entity.CreatedDate = (DateTime)row["CreatedDate"];
            entity.ModifiedByID = !DBNull.Value.Equals(row["ModifiedByID"]) ? (long?)row["ModifiedByID"] : null;
            entity.ModifiedDate = !DBNull.Value.Equals(row["ModifiedDate"]) ? (DateTime?)row["ModifiedDate"] : null;

            return entity;

        }
        #endregion
    }
}
