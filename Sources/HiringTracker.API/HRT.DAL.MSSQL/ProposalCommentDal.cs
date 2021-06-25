

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

        public ProposalComment Get(System.Int64 ProposalID,System.Int64 CommentID)
        {
            ProposalComment result = default(ProposalComment);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_ProposalComment_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ProposalID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ProposalID);
            
                            AddParameter(   cmd, "@CommentID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, CommentID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = ProposalCommentFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64 ProposalID,System.Int64 CommentID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_ProposalComment_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ProposalID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ProposalID);
            
                            AddParameter(   cmd, "@CommentID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, CommentID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
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

        public ProposalComment Insert(ProposalComment entity) 
        {
            ProposalComment entityOut = base.Upsert<ProposalComment>("p_ProposalComment_Insert", entity, AddUpsertParameters, ProposalCommentFromRow);

            return entityOut;
        }

        public ProposalComment Update(ProposalComment entity) 
        {
            ProposalComment entityOut = base.Upsert<ProposalComment>("p_ProposalComment_Update", entity, AddUpsertParameters, ProposalCommentFromRow);

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

                    entity.ProposalID = !DBNull.Value.Equals(row["ProposalID"]) ? (System.Int64)row["ProposalID"] : default(System.Int64);
                    entity.CommentID = !DBNull.Value.Equals(row["CommentID"]) ? (System.Int64)row["CommentID"] : default(System.Int64);
        
            return entity;
        }
        
    }
}
