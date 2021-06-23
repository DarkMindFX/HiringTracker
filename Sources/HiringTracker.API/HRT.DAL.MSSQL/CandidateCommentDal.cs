

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
    class CandidateCommentDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(ICandidateCommentDal))]
    public class CandidateCommentDal: SQLDal, ICandidateCommentDal
    {
        public IInitParams CreateInitParams()
        {
            return new CandidateCommentDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<CandidateComment>("p_CandidateComment_Delete", id, "@ID");

            return removed;
        }

        public CandidateComment Get(long id)
        {
            CandidateComment entityOut = base.Get<CandidateComment>("p_CandidateComment_GetDetails", id, "@ID", CandidateCommentFromRow);

            return entityOut;
        }

                public IList<CandidateComment> GetByCandidateID(System.Int64 CandidateID)
        {
            var entitiesOut = base.GetBy<CandidateComment, System.Int64>("p_CandidateComment_GetByCandidateID", CandidateID, "@CandidateID", SqlDbType.BigInt, 0, CandidateCommentFromRow);

            return entitiesOut;
        }
                public IList<CandidateComment> GetByCommentID(System.Int64 CommentID)
        {
            var entitiesOut = base.GetBy<CandidateComment, System.Int64>("p_CandidateComment_GetByCommentID", CommentID, "@CommentID", SqlDbType.BigInt, 0, CandidateCommentFromRow);

            return entitiesOut;
        }
        
        public IList<CandidateComment> GetAll()
        {
            IList<CandidateComment> result = base.GetAll<CandidateComment>("p_CandidateComment_GetAll", CandidateCommentFromRow);

            return result;
        }

        public CandidateComment Upsert(CandidateComment entity) 
        {
            CandidateComment entityOut = base.Upsert<CandidateComment>("p_CandidateComment_Upsert", entity, AddUpsertParameters, CandidateCommentFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, CandidateComment entity)
        {
                SqlParameter pCandidateID = new SqlParameter("@CandidateID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CandidateID", DataRowVersion.Current, (object)entity.CandidateID != null ? (object)entity.CandidateID : DBNull.Value);   cmd.Parameters.Add(pCandidateID); 
                SqlParameter pCommentID = new SqlParameter("@CommentID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CommentID", DataRowVersion.Current, (object)entity.CommentID != null ? (object)entity.CommentID : DBNull.Value);   cmd.Parameters.Add(pCommentID); 
        
            return cmd;
        }

        protected CandidateComment CandidateCommentFromRow(DataRow row)
        {
            var entity = new CandidateComment();

                    entity.CandidateID = (System.Int64)row["CandidateID"];
                    entity.CommentID = (System.Int64)row["CommentID"];
        
            return entity;
        }
        
    }
}
