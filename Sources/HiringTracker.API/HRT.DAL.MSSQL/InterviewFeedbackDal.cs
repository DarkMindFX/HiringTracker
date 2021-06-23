

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
    class InterviewFeedbackDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IInterviewFeedbackDal))]
    public class InterviewFeedbackDal: SQLDal, IInterviewFeedbackDal
    {
        public IInitParams CreateInitParams()
        {
            return new InterviewFeedbackDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<InterviewFeedback>("p_InterviewFeedback_Delete", id, "@ID");

            return removed;
        }

        public InterviewFeedback Get(long id)
        {
            InterviewFeedback entityOut = base.Get<InterviewFeedback>("p_InterviewFeedback_GetDetails", id, "@ID", InterviewFeedbackFromRow);

            return entityOut;
        }

                public IList<InterviewFeedback> GetByInterviewID(System.Int64 InterviewID)
        {
            var entitiesOut = base.GetBy<InterviewFeedback, System.Int64>("p_InterviewFeedback_GetByInterviewID", InterviewID, "@InterviewID", SqlDbType.BigInt, 0, InterviewFeedbackFromRow);

            return entitiesOut;
        }
                public IList<InterviewFeedback> GetByInterviewerID(System.Int64 InterviewerID)
        {
            var entitiesOut = base.GetBy<InterviewFeedback, System.Int64>("p_InterviewFeedback_GetByInterviewerID", InterviewerID, "@InterviewerID", SqlDbType.BigInt, 0, InterviewFeedbackFromRow);

            return entitiesOut;
        }
                public IList<InterviewFeedback> GetByCreatedByID(System.Int64 CreatedByID)
        {
            var entitiesOut = base.GetBy<InterviewFeedback, System.Int64>("p_InterviewFeedback_GetByCreatedByID", CreatedByID, "@CreatedByID", SqlDbType.BigInt, 0, InterviewFeedbackFromRow);

            return entitiesOut;
        }
                public IList<InterviewFeedback> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            var entitiesOut = base.GetBy<InterviewFeedback, System.Int64?>("p_InterviewFeedback_GetByModifiedByID", ModifiedByID, "@ModifiedByID", SqlDbType.BigInt, 0, InterviewFeedbackFromRow);

            return entitiesOut;
        }
        
        public IList<InterviewFeedback> GetAll()
        {
            IList<InterviewFeedback> result = base.GetAll<InterviewFeedback>("p_InterviewFeedback_GetAll", InterviewFeedbackFromRow);

            return result;
        }

        public InterviewFeedback Upsert(InterviewFeedback entity) 
        {
            InterviewFeedback entityOut = base.Upsert<InterviewFeedback>("p_InterviewFeedback_Upsert", entity, AddUpsertParameters, InterviewFeedbackFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, InterviewFeedback entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pComment = new SqlParameter("@Comment", System.Data.SqlDbType.NVarChar, 4000, ParameterDirection.Input, false, 0, 0, "Comment", DataRowVersion.Current, (object)entity.Comment != null ? (object)entity.Comment : DBNull.Value);   cmd.Parameters.Add(pComment); 
                SqlParameter pRating = new SqlParameter("@Rating", System.Data.SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "Rating", DataRowVersion.Current, (object)entity.Rating != null ? (object)entity.Rating : DBNull.Value);   cmd.Parameters.Add(pRating); 
                SqlParameter pInterviewID = new SqlParameter("@InterviewID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "InterviewID", DataRowVersion.Current, (object)entity.InterviewID != null ? (object)entity.InterviewID : DBNull.Value);   cmd.Parameters.Add(pInterviewID); 
                SqlParameter pInterviewerID = new SqlParameter("@InterviewerID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "InterviewerID", DataRowVersion.Current, (object)entity.InterviewerID != null ? (object)entity.InterviewerID : DBNull.Value);   cmd.Parameters.Add(pInterviewerID); 
                SqlParameter pCreatedByID = new SqlParameter("@CreatedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CreatedByID", DataRowVersion.Current, (object)entity.CreatedByID != null ? (object)entity.CreatedByID : DBNull.Value);   cmd.Parameters.Add(pCreatedByID); 
                SqlParameter pCreatedDate = new SqlParameter("@CreatedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CreatedDate", DataRowVersion.Current, (object)entity.CreatedDate != null ? (object)entity.CreatedDate : DBNull.Value);   cmd.Parameters.Add(pCreatedDate); 
                SqlParameter pModifiedByID = new SqlParameter("@ModifiedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ModifiedByID", DataRowVersion.Current, (object)entity.ModifiedByID != null ? (object)entity.ModifiedByID : DBNull.Value);   cmd.Parameters.Add(pModifiedByID); 
                SqlParameter pModifiedDate = new SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ModifiedDate", DataRowVersion.Current, (object)entity.ModifiedDate != null ? (object)entity.ModifiedDate : DBNull.Value);   cmd.Parameters.Add(pModifiedDate); 
        
            return cmd;
        }

        protected InterviewFeedback InterviewFeedbackFromRow(DataRow row)
        {
            var entity = new InterviewFeedback();

                    entity.ID = (System.Int64)row["ID"];
                    entity.Comment = (System.String)row["Comment"];
                    entity.Rating = (System.Int32)row["Rating"];
                    entity.InterviewID = (System.Int64)row["InterviewID"];
                    entity.InterviewerID = (System.Int64)row["InterviewerID"];
                    entity.CreatedByID = (System.Int64)row["CreatedByID"];
                    entity.CreatedDate = (System.DateTime)row["CreatedDate"];
                    entity.ModifiedByID = (System.Int64?)row["ModifiedByID"];
                    entity.ModifiedDate = (System.DateTime?)row["ModifiedDate"];
        
            return entity;
        }
        
    }
}
