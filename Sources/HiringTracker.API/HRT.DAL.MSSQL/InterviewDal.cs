

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
    class InterviewDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IInterviewDal))]
    public class InterviewDal: SQLDal, IInterviewDal
    {
        public IInitParams CreateInitParams()
        {
            return new InterviewDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public Interview Get(System.Int64? ID)
        {
            Interview result = default(Interview);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Interview_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = InterviewFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Interview_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<Interview> GetByProposalID(System.Int64 ProposalID)
        {
            var entitiesOut = base.GetBy<Interview, System.Int64>("p_Interview_GetByProposalID", ProposalID, "@ProposalID", SqlDbType.BigInt, 0, InterviewFromRow);

            return entitiesOut;
        }
                public IList<Interview> GetByInterviewTypeID(System.Int64 InterviewTypeID)
        {
            var entitiesOut = base.GetBy<Interview, System.Int64>("p_Interview_GetByInterviewTypeID", InterviewTypeID, "@InterviewTypeID", SqlDbType.BigInt, 0, InterviewFromRow);

            return entitiesOut;
        }
                public IList<Interview> GetByInterviewStatusID(System.Int64 InterviewStatusID)
        {
            var entitiesOut = base.GetBy<Interview, System.Int64>("p_Interview_GetByInterviewStatusID", InterviewStatusID, "@InterviewStatusID", SqlDbType.BigInt, 0, InterviewFromRow);

            return entitiesOut;
        }
                public IList<Interview> GetByCreatedByID(System.Int64 CreatedByID)
        {
            var entitiesOut = base.GetBy<Interview, System.Int64>("p_Interview_GetByCreatedByID", CreatedByID, "@CreatedByID", SqlDbType.BigInt, 0, InterviewFromRow);

            return entitiesOut;
        }
                public IList<Interview> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            var entitiesOut = base.GetBy<Interview, System.Int64?>("p_Interview_GetByModifiedByID", ModifiedByID, "@ModifiedByID", SqlDbType.BigInt, 0, InterviewFromRow);

            return entitiesOut;
        }
        
        public IList<Interview> GetAll()
        {
            IList<Interview> result = base.GetAll<Interview>("p_Interview_GetAll", InterviewFromRow);

            return result;
        }

        public Interview Upsert(Interview entity) 
        {
            Interview entityOut = base.Upsert<Interview>("p_Interview_Upsert", entity, AddUpsertParameters, InterviewFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Interview entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pProposalID = new SqlParameter("@ProposalID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ProposalID", DataRowVersion.Current, (object)entity.ProposalID != null ? (object)entity.ProposalID : DBNull.Value);   cmd.Parameters.Add(pProposalID); 
                SqlParameter pInterviewTypeID = new SqlParameter("@InterviewTypeID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "InterviewTypeID", DataRowVersion.Current, (object)entity.InterviewTypeID != null ? (object)entity.InterviewTypeID : DBNull.Value);   cmd.Parameters.Add(pInterviewTypeID); 
                SqlParameter pStartTime = new SqlParameter("@StartTime", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "StartTime", DataRowVersion.Current, (object)entity.StartTime != null ? (object)entity.StartTime : DBNull.Value);   cmd.Parameters.Add(pStartTime); 
                SqlParameter pEndTime = new SqlParameter("@EndTime", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "EndTime", DataRowVersion.Current, (object)entity.EndTime != null ? (object)entity.EndTime : DBNull.Value);   cmd.Parameters.Add(pEndTime); 
                SqlParameter pInterviewStatusID = new SqlParameter("@InterviewStatusID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "InterviewStatusID", DataRowVersion.Current, (object)entity.InterviewStatusID != null ? (object)entity.InterviewStatusID : DBNull.Value);   cmd.Parameters.Add(pInterviewStatusID); 
                SqlParameter pCreatedByID = new SqlParameter("@CreatedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CreatedByID", DataRowVersion.Current, (object)entity.CreatedByID != null ? (object)entity.CreatedByID : DBNull.Value);   cmd.Parameters.Add(pCreatedByID); 
                SqlParameter pCretedDate = new SqlParameter("@CretedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CretedDate", DataRowVersion.Current, (object)entity.CretedDate != null ? (object)entity.CretedDate : DBNull.Value);   cmd.Parameters.Add(pCretedDate); 
                SqlParameter pModifiedByID = new SqlParameter("@ModifiedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ModifiedByID", DataRowVersion.Current, (object)entity.ModifiedByID != null ? (object)entity.ModifiedByID : DBNull.Value);   cmd.Parameters.Add(pModifiedByID); 
                SqlParameter pModifiedDate = new SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ModifiedDate", DataRowVersion.Current, (object)entity.ModifiedDate != null ? (object)entity.ModifiedDate : DBNull.Value);   cmd.Parameters.Add(pModifiedDate); 
        
            return cmd;
        }

        protected Interview InterviewFromRow(DataRow row)
        {
            var entity = new Interview();

                    entity.ID = (System.Int64?)row["ID"];
                    entity.ProposalID = (System.Int64)row["ProposalID"];
                    entity.InterviewTypeID = (System.Int64)row["InterviewTypeID"];
                    entity.StartTime = (System.DateTime)row["StartTime"];
                    entity.EndTime = (System.DateTime)row["EndTime"];
                    entity.InterviewStatusID = (System.Int64)row["InterviewStatusID"];
                    entity.CreatedByID = (System.Int64)row["CreatedByID"];
                    entity.CretedDate = (System.DateTime)row["CretedDate"];
                    entity.ModifiedByID = (System.Int64?)row["ModifiedByID"];
                    entity.ModifiedDate = (System.DateTime?)row["ModifiedDate"];
        
            return entity;
        }
        
    }
}
