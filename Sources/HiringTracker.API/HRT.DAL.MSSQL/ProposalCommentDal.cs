

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
    class ProposalCommentDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IProposalCommentDal))]
    public class ProposalCommentDal: SQLDal, IProposalCommentDal
    {
        public IInitParams CreateInitParams()
        {
            return new ProposalCommentDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<ProposalComment>("p_ProposalComment_Delete", id, "@ID");

            return removed;
        }

        public ProposalComment Get(long id)
        {
            ProposalComment entityOut = base.Get<ProposalComment>("p_ProposalComment_GetDetails", id, "@ID", ProposalCommentFromRow);

            return entityOut;
        }

                public IList<ProposalComment> GetByProposalID(System.Int64 ProposalID)
        {
            var entitiesOut = base.GetBy<ProposalComment, System.Int64>("p_ProposalComment_GetByProposalID", ProposalID, "@ProposalID", SqlDbType.BigInt, 0, ProposalCommentFromRow);

            return entitiesOut;
        }
                public IList<ProposalComment> GetByCommentID(System.Int64 CommentID)
        {
            var entitiesOut = base.GetBy<ProposalComment, System.Int64>("p_ProposalComment_GetByCommentID", CommentID, "@CommentID", SqlDbType.BigInt, 0, ProposalCommentFromRow);

            return entitiesOut;
        }
        
        public IList<ProposalComment> GetAll()
        {
            IList<ProposalComment> result = base.GetAll<ProposalComment>("p_ProposalComment_GetAll", ProposalCommentFromRow);

            return result;
        }

        public ProposalComment Upsert(ProposalComment entity) 
        {
            ProposalComment entityOut = base.Upsert<ProposalComment>("p_ProposalComment_Upsert", entity, AddUpsertParameters, ProposalCommentFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, ProposalComment entity)
        {
                SqlParameter pProposalID = new SqlParameter("@ProposalID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ProposalID", DataRowVersion.Current, (object)entity.ProposalID != null ? (object)entity.ProposalID : DBNull.Value);   cmd.Parameters.Add(pProposalID); 
                SqlParameter pCommentID = new SqlParameter("@CommentID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CommentID", DataRowVersion.Current, (object)entity.CommentID != null ? (object)entity.CommentID : DBNull.Value);   cmd.Parameters.Add(pCommentID); 
        
            return cmd;
        }

        protected ProposalComment ProposalCommentFromRow(DataRow row)
        {
            var entity = new ProposalComment();

                    entity.ProposalID = (System.Int64)row["ProposalID"];
                    entity.CommentID = (System.Int64)row["CommentID"];
        
            return entity;
        }
        
    }
}
