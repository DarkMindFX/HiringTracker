

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

        public InterviewFeedback Get(System.Int64 ID)
        {
            InterviewFeedback result = default(InterviewFeedback);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_InterviewFeedback_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = InterviewFeedbackFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64 ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_InterviewFeedback_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
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

        public InterviewFeedback Insert(InterviewFeedback entity) 
        {
            InterviewFeedback entityOut = base.Upsert<InterviewFeedback>("p_InterviewFeedback_Insert", entity, AddUpsertParameters, InterviewFeedbackFromRow);

            return entityOut;
        }

        public InterviewFeedback Update(InterviewFeedback entity) 
        {
            InterviewFeedback entityOut = base.Upsert<InterviewFeedback>("p_InterviewFeedback_Update", entity, AddUpsertParameters, InterviewFeedbackFromRow);

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

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64)row["ID"] : default(System.Int64);
                    entity.Comment = !DBNull.Value.Equals(row["Comment"]) ? (System.String)row["Comment"] : default(System.String);
                    entity.Rating = !DBNull.Value.Equals(row["Rating"]) ? (System.Int32)row["Rating"] : default(System.Int32);
                    entity.InterviewID = !DBNull.Value.Equals(row["InterviewID"]) ? (System.Int64)row["InterviewID"] : default(System.Int64);
                    entity.InterviewerID = !DBNull.Value.Equals(row["InterviewerID"]) ? (System.Int64)row["InterviewerID"] : default(System.Int64);
                    entity.CreatedByID = !DBNull.Value.Equals(row["CreatedByID"]) ? (System.Int64)row["CreatedByID"] : default(System.Int64);
                    entity.CreatedDate = !DBNull.Value.Equals(row["CreatedDate"]) ? (System.DateTime)row["CreatedDate"] : default(System.DateTime);
                    entity.ModifiedByID = !DBNull.Value.Equals(row["ModifiedByID"]) ? (System.Int64?)row["ModifiedByID"] : default(System.Int64?);
                    entity.ModifiedDate = !DBNull.Value.Equals(row["ModifiedDate"]) ? (System.DateTime?)row["ModifiedDate"] : default(System.DateTime?);
        
            return entity;
        }
        
    }
}
