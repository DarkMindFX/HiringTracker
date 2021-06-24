

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

        public PositionComment Get(System.Int64 PositionID,System.Int64 CommentID)
        {
            PositionComment result = default(PositionComment);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_PositionComment_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@PositionID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, PositionID);
            
                            AddParameter(   cmd, "@CommentID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, CommentID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = PositionCommentFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64 PositionID,System.Int64 CommentID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_PositionComment_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@PositionID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, PositionID);
            
                            AddParameter(   cmd, "@CommentID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, CommentID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
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
