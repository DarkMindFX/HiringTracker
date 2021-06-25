

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
    class ProposalDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IProposalDal))]
    public class ProposalDal: SQLDal, IProposalDal
    {
        public IInitParams CreateInitParams()
        {
            return new ProposalDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public Proposal Get(System.Int64? ID)
        {
            Proposal result = default(Proposal);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Proposal_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = ProposalFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Proposal_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<Proposal> GetByPositionID(System.Int64 PositionID)
        {
            var entitiesOut = base.GetBy<Proposal, System.Int64>("p_Proposal_GetByPositionID", PositionID, "@PositionID", SqlDbType.BigInt, 0, ProposalFromRow);

            return entitiesOut;
        }
                public IList<Proposal> GetByCandidateID(System.Int64 CandidateID)
        {
            var entitiesOut = base.GetBy<Proposal, System.Int64>("p_Proposal_GetByCandidateID", CandidateID, "@CandidateID", SqlDbType.BigInt, 0, ProposalFromRow);

            return entitiesOut;
        }
                public IList<Proposal> GetByCurrentStepID(System.Int64 CurrentStepID)
        {
            var entitiesOut = base.GetBy<Proposal, System.Int64>("p_Proposal_GetByCurrentStepID", CurrentStepID, "@CurrentStepID", SqlDbType.BigInt, 0, ProposalFromRow);

            return entitiesOut;
        }
                public IList<Proposal> GetByNextStepID(System.Int64? NextStepID)
        {
            var entitiesOut = base.GetBy<Proposal, System.Int64?>("p_Proposal_GetByNextStepID", NextStepID, "@NextStepID", SqlDbType.BigInt, 0, ProposalFromRow);

            return entitiesOut;
        }
                public IList<Proposal> GetByStatusID(System.Int64 StatusID)
        {
            var entitiesOut = base.GetBy<Proposal, System.Int64>("p_Proposal_GetByStatusID", StatusID, "@StatusID", SqlDbType.BigInt, 0, ProposalFromRow);

            return entitiesOut;
        }
                public IList<Proposal> GetByCreatedByID(System.Int64? CreatedByID)
        {
            var entitiesOut = base.GetBy<Proposal, System.Int64?>("p_Proposal_GetByCreatedByID", CreatedByID, "@CreatedByID", SqlDbType.BigInt, 0, ProposalFromRow);

            return entitiesOut;
        }
                public IList<Proposal> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            var entitiesOut = base.GetBy<Proposal, System.Int64?>("p_Proposal_GetByModifiedByID", ModifiedByID, "@ModifiedByID", SqlDbType.BigInt, 0, ProposalFromRow);

            return entitiesOut;
        }
        
        public IList<Proposal> GetAll()
        {
            IList<Proposal> result = base.GetAll<Proposal>("p_Proposal_GetAll", ProposalFromRow);

            return result;
        }

        public Proposal Insert(Proposal entity) 
        {
            Proposal entityOut = base.Upsert<Proposal>("p_Proposal_Insert", entity, AddUpsertParameters, ProposalFromRow);

            return entityOut;
        }

        public Proposal Update(Proposal entity) 
        {
            Proposal entityOut = base.Upsert<Proposal>("p_Proposal_Update", entity, AddUpsertParameters, ProposalFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Proposal entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pPositionID = new SqlParameter("@PositionID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "PositionID", DataRowVersion.Current, (object)entity.PositionID != null ? (object)entity.PositionID : DBNull.Value);   cmd.Parameters.Add(pPositionID); 
                SqlParameter pCandidateID = new SqlParameter("@CandidateID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CandidateID", DataRowVersion.Current, (object)entity.CandidateID != null ? (object)entity.CandidateID : DBNull.Value);   cmd.Parameters.Add(pCandidateID); 
                SqlParameter pProposed = new SqlParameter("@Proposed", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "Proposed", DataRowVersion.Current, (object)entity.Proposed != null ? (object)entity.Proposed : DBNull.Value);   cmd.Parameters.Add(pProposed); 
                SqlParameter pCurrentStepID = new SqlParameter("@CurrentStepID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CurrentStepID", DataRowVersion.Current, (object)entity.CurrentStepID != null ? (object)entity.CurrentStepID : DBNull.Value);   cmd.Parameters.Add(pCurrentStepID); 
                SqlParameter pStepSetDate = new SqlParameter("@StepSetDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "StepSetDate", DataRowVersion.Current, (object)entity.StepSetDate != null ? (object)entity.StepSetDate : DBNull.Value);   cmd.Parameters.Add(pStepSetDate); 
                SqlParameter pNextStepID = new SqlParameter("@NextStepID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "NextStepID", DataRowVersion.Current, (object)entity.NextStepID != null ? (object)entity.NextStepID : DBNull.Value);   cmd.Parameters.Add(pNextStepID); 
                SqlParameter pDueDate = new SqlParameter("@DueDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DueDate", DataRowVersion.Current, (object)entity.DueDate != null ? (object)entity.DueDate : DBNull.Value);   cmd.Parameters.Add(pDueDate); 
                SqlParameter pStatusID = new SqlParameter("@StatusID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "StatusID", DataRowVersion.Current, (object)entity.StatusID != null ? (object)entity.StatusID : DBNull.Value);   cmd.Parameters.Add(pStatusID); 
                SqlParameter pCreatedByID = new SqlParameter("@CreatedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CreatedByID", DataRowVersion.Current, (object)entity.CreatedByID != null ? (object)entity.CreatedByID : DBNull.Value);   cmd.Parameters.Add(pCreatedByID); 
                SqlParameter pCreatedDate = new SqlParameter("@CreatedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CreatedDate", DataRowVersion.Current, (object)entity.CreatedDate != null ? (object)entity.CreatedDate : DBNull.Value);   cmd.Parameters.Add(pCreatedDate); 
                SqlParameter pModifiedByID = new SqlParameter("@ModifiedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ModifiedByID", DataRowVersion.Current, (object)entity.ModifiedByID != null ? (object)entity.ModifiedByID : DBNull.Value);   cmd.Parameters.Add(pModifiedByID); 
                SqlParameter pModifiedDate = new SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ModifiedDate", DataRowVersion.Current, (object)entity.ModifiedDate != null ? (object)entity.ModifiedDate : DBNull.Value);   cmd.Parameters.Add(pModifiedDate); 
        
            return cmd;
        }

        protected Proposal ProposalFromRow(DataRow row)
        {
            var entity = new Proposal();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.PositionID = !DBNull.Value.Equals(row["PositionID"]) ? (System.Int64)row["PositionID"] : default(System.Int64);
                    entity.CandidateID = !DBNull.Value.Equals(row["CandidateID"]) ? (System.Int64)row["CandidateID"] : default(System.Int64);
                    entity.Proposed = !DBNull.Value.Equals(row["Proposed"]) ? (System.DateTime)row["Proposed"] : default(System.DateTime);
                    entity.CurrentStepID = !DBNull.Value.Equals(row["CurrentStepID"]) ? (System.Int64)row["CurrentStepID"] : default(System.Int64);
                    entity.StepSetDate = !DBNull.Value.Equals(row["StepSetDate"]) ? (System.DateTime)row["StepSetDate"] : default(System.DateTime);
                    entity.NextStepID = !DBNull.Value.Equals(row["NextStepID"]) ? (System.Int64?)row["NextStepID"] : default(System.Int64?);
                    entity.DueDate = !DBNull.Value.Equals(row["DueDate"]) ? (System.DateTime?)row["DueDate"] : default(System.DateTime?);
                    entity.StatusID = !DBNull.Value.Equals(row["StatusID"]) ? (System.Int64)row["StatusID"] : default(System.Int64);
                    entity.CreatedByID = !DBNull.Value.Equals(row["CreatedByID"]) ? (System.Int64?)row["CreatedByID"] : default(System.Int64?);
                    entity.CreatedDate = !DBNull.Value.Equals(row["CreatedDate"]) ? (System.DateTime?)row["CreatedDate"] : default(System.DateTime?);
                    entity.ModifiedByID = !DBNull.Value.Equals(row["ModifiedByID"]) ? (System.Int64?)row["ModifiedByID"] : default(System.Int64?);
                    entity.ModifiedDate = !DBNull.Value.Equals(row["ModifiedDate"]) ? (System.DateTime?)row["ModifiedDate"] : default(System.DateTime?);
        
            return entity;
        }
        
    }
}
