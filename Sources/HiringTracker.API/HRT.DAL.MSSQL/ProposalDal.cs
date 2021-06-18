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

        public bool Delete(long id)
        {
            bool removed = base.Delete<Proposal>("p_Proposal_Delete", id, "@ID");

            return removed;
        }

        public Proposal Get(long id)
        {
            Proposal entityOut = base.Get<Proposal>("p_Proposal_GetDetails", id, "@ID", ProposalFromRow);

            return entityOut;
        }

        public IList<Proposal> GetAll()
        {
            IList<Proposal> result = base.GetAll<Proposal>("p_Proposal_GetAll", ProposalFromRow);

            return result;
        }

        public Proposal Upsert(Proposal entity) 
        {
            Proposal entityOut = base.Upsert<Proposal>("p_Proposal_Upsert", entity, AddUpsertParameters, ProposalFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Proposal entity)
        {
            		   SqlParameter pID = new SqlParameter(@"ID",    SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 

		   SqlParameter pPositionID = new SqlParameter(@"PositionID",    SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "PositionID", DataRowVersion.Current, (object)entity.PositionID != null ? (object)entity.PositionID : DBNull.Value);   cmd.Parameters.Add(pPositionID); 

		   SqlParameter pCandidateID = new SqlParameter(@"CandidateID",    SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CandidateID", DataRowVersion.Current, (object)entity.CandidateID != null ? (object)entity.CandidateID : DBNull.Value);   cmd.Parameters.Add(pCandidateID); 

		   SqlParameter pProposed = new SqlParameter(@"Proposed",    SqlDbType.DateTime2, 0, ParameterDirection.Input, false, 0, 0, "Proposed", DataRowVersion.Current, (object)entity.Proposed != null ? (object)entity.Proposed : DBNull.Value);   cmd.Parameters.Add(pProposed); 

		   SqlParameter pCurrentStepID = new SqlParameter(@"CurrentStepID",    SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CurrentStepID", DataRowVersion.Current, (object)entity.CurrentStepID != null ? (object)entity.CurrentStepID : DBNull.Value);   cmd.Parameters.Add(pCurrentStepID); 

		   SqlParameter pStepSetDate = new SqlParameter(@"StepSetDate",    SqlDbType.DateTime2, 0, ParameterDirection.Input, false, 0, 0, "StepSetDate", DataRowVersion.Current, (object)entity.StepSetDate != null ? (object)entity.StepSetDate : DBNull.Value);   cmd.Parameters.Add(pStepSetDate); 

		   SqlParameter pNextStepID = new SqlParameter(@"NextStepID",    SqlDbType.BigInt, 0, ParameterDirection.Input, true, 0, 0, "NextStepID", DataRowVersion.Current, (object)entity.NextStepID != null ? (object)entity.NextStepID : DBNull.Value);   cmd.Parameters.Add(pNextStepID); 

		   SqlParameter pDueDate = new SqlParameter(@"DueDate",    SqlDbType.DateTime2, 0, ParameterDirection.Input, true, 0, 0, "DueDate", DataRowVersion.Current, (object)entity.DueDate != null ? (object)entity.DueDate : DBNull.Value);   cmd.Parameters.Add(pDueDate); 

		   SqlParameter pStatusID = new SqlParameter(@"StatusID",    SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "StatusID", DataRowVersion.Current, (object)entity.StatusID != null ? (object)entity.StatusID : DBNull.Value);   cmd.Parameters.Add(pStatusID); 

		   SqlParameter pCreatedByID = new SqlParameter(@"CreatedByID",    SqlDbType.BigInt, 0, ParameterDirection.Input, true, 0, 0, "CreatedByID", DataRowVersion.Current, (object)entity.CreatedByID != null ? (object)entity.CreatedByID : DBNull.Value);   cmd.Parameters.Add(pCreatedByID); 

		   SqlParameter pCreatedDate = new SqlParameter(@"CreatedDate",    SqlDbType.DateTime2, 0, ParameterDirection.Input, true, 0, 0, "CreatedDate", DataRowVersion.Current, (object)entity.CreatedDate != null ? (object)entity.CreatedDate : DBNull.Value);   cmd.Parameters.Add(pCreatedDate); 

		   SqlParameter pModifiedByID = new SqlParameter(@"ModifiedByID",    SqlDbType.BigInt, 0, ParameterDirection.Input, true, 0, 0, "ModifiedByID", DataRowVersion.Current, (object)entity.ModifiedByID != null ? (object)entity.ModifiedByID : DBNull.Value);   cmd.Parameters.Add(pModifiedByID); 

		   SqlParameter pModifiedDate = new SqlParameter(@"ModifiedDate",    SqlDbType.DateTime2, 0, ParameterDirection.Input, true, 0, 0, "ModifiedDate", DataRowVersion.Current, (object)entity.ModifiedDate != null ? (object)entity.ModifiedDate : DBNull.Value);   cmd.Parameters.Add(pModifiedDate); 



            return cmd;
        }

        protected Proposal ProposalFromRow(DataRow row)
        {
            var entity = new Proposal();

            		entity.ID = (System.Int64?)row["ID"];
		entity.PositionID = (System.Int64)row["PositionID"];
		entity.CandidateID = (System.Int64)row["CandidateID"];
		entity.Proposed = (System.DateTime)row["Proposed"];
		entity.CurrentStepID = (System.Int64)row["CurrentStepID"];
		entity.StepSetDate = (System.DateTime)row["StepSetDate"];
		entity.NextStepID = !DBNull.Value.Equals(row["NextStepID"]) ?  (System.Int64?)row["NextStepID"] : null;
		entity.DueDate = !DBNull.Value.Equals(row["DueDate"]) ?  (System.DateTime?)row["DueDate"] : null;
		entity.StatusID = (System.Int64)row["StatusID"];
		entity.CreatedByID = !DBNull.Value.Equals(row["CreatedByID"]) ?  (System.Int64?)row["CreatedByID"] : null;
		entity.CreatedDate = !DBNull.Value.Equals(row["CreatedDate"]) ?  (System.DateTime?)row["CreatedDate"] : null;
		entity.ModifiedByID = !DBNull.Value.Equals(row["ModifiedByID"]) ?  (System.Int64?)row["ModifiedByID"] : null;
		entity.ModifiedDate = !DBNull.Value.Equals(row["ModifiedDate"]) ?  (System.DateTime?)row["ModifiedDate"] : null;


            return entity;
        }

        public long? Upsert(Proposal entity, long? editorID)
        {
            throw new NotImplementedException();
        }

        public IList<Proposal> GetByCandidate(long id)
        {
            throw new NotImplementedException();
        }

        public IList<Proposal> GetByPosition(long id)
        {
            throw new NotImplementedException();
        }
    }
}
