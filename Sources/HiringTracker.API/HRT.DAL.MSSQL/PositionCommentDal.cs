

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
    class PositionCommentDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IPositionCommentDal))]
    public class PositionCommentDal: SQLDal, IPositionCommentDal
    {
        public IInitParams CreateInitParams()
        {
            return new PositionCommentDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<PositionComment>("p_PositionComment_Delete", id, "@ID");

            return removed;
        }

        public PositionComment Get(long id)
        {
            PositionComment entityOut = base.Get<PositionComment>("p_PositionComment_GetDetails", id, "@ID", PositionCommentFromRow);

            return entityOut;
        }

                public IList<PositionComment> GetByPositionID(System.Int64 PositionID)
        {
            var entitiesOut = base.GetBy<PositionComment, System.Int64>("p_PositionComment_GetByPositionID", PositionID, "@PositionID", SqlDbType.BigInt, 0, PositionCommentFromRow);

            return entitiesOut;
        }
                public IList<PositionComment> GetByCommentID(System.Int64 CommentID)
        {
            var entitiesOut = base.GetBy<PositionComment, System.Int64>("p_PositionComment_GetByCommentID", CommentID, "@CommentID", SqlDbType.BigInt, 0, PositionCommentFromRow);

            return entitiesOut;
        }
        
        public IList<PositionComment> GetAll()
        {
            IList<PositionComment> result = base.GetAll<PositionComment>("p_PositionComment_GetAll", PositionCommentFromRow);

            return result;
        }

        public PositionComment Upsert(PositionComment entity) 
        {
            PositionComment entityOut = base.Upsert<PositionComment>("p_PositionComment_Upsert", entity, AddUpsertParameters, PositionCommentFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, PositionComment entity)
        {
                SqlParameter pPositionID = new SqlParameter("@PositionID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "PositionID", DataRowVersion.Current, (object)entity.PositionID != null ? (object)entity.PositionID : DBNull.Value);   cmd.Parameters.Add(pPositionID); 
                SqlParameter pCommentID = new SqlParameter("@CommentID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CommentID", DataRowVersion.Current, (object)entity.CommentID != null ? (object)entity.CommentID : DBNull.Value);   cmd.Parameters.Add(pCommentID); 
        
            return cmd;
        }

        protected PositionComment PositionCommentFromRow(DataRow row)
        {
            var entity = new PositionComment();

                    entity.PositionID = (System.Int64)row["PositionID"];
                    entity.CommentID = (System.Int64)row["CommentID"];
        
            return entity;
        }
        
    }
}
