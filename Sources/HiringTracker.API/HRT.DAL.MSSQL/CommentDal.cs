

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
    class CommentDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(ICommentDal))]
    public class CommentDal: SQLDal, ICommentDal
    {
        public IInitParams CreateInitParams()
        {
            return new CommentDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        Comment Get(System.Int64? ID)
        {
            Comment result = default(Comment);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Comment_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = CommentFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Comment_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<Comment> GetByCreatedByID(System.Int64 CreatedByID)
        {
            var entitiesOut = base.GetBy<Comment, System.Int64>("p_Comment_GetByCreatedByID", CreatedByID, "@CreatedByID", SqlDbType.BigInt, 0, CommentFromRow);

            return entitiesOut;
        }
                public IList<Comment> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            var entitiesOut = base.GetBy<Comment, System.Int64?>("p_Comment_GetByModifiedByID", ModifiedByID, "@ModifiedByID", SqlDbType.BigInt, 0, CommentFromRow);

            return entitiesOut;
        }
        
        public IList<Comment> GetAll()
        {
            IList<Comment> result = base.GetAll<Comment>("p_Comment_GetAll", CommentFromRow);

            return result;
        }

        public Comment Upsert(Comment entity) 
        {
            Comment entityOut = base.Upsert<Comment>("p_Comment_Upsert", entity, AddUpsertParameters, CommentFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Comment entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pText = new SqlParameter("@Text", System.Data.SqlDbType.NVarChar, 4000, ParameterDirection.Input, false, 0, 0, "Text", DataRowVersion.Current, (object)entity.Text != null ? (object)entity.Text : DBNull.Value);   cmd.Parameters.Add(pText); 
                SqlParameter pCreatedDate = new SqlParameter("@CreatedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CreatedDate", DataRowVersion.Current, (object)entity.CreatedDate != null ? (object)entity.CreatedDate : DBNull.Value);   cmd.Parameters.Add(pCreatedDate); 
                SqlParameter pCreatedByID = new SqlParameter("@CreatedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CreatedByID", DataRowVersion.Current, (object)entity.CreatedByID != null ? (object)entity.CreatedByID : DBNull.Value);   cmd.Parameters.Add(pCreatedByID); 
                SqlParameter pModifiedDate = new SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ModifiedDate", DataRowVersion.Current, (object)entity.ModifiedDate != null ? (object)entity.ModifiedDate : DBNull.Value);   cmd.Parameters.Add(pModifiedDate); 
                SqlParameter pModifiedByID = new SqlParameter("@ModifiedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ModifiedByID", DataRowVersion.Current, (object)entity.ModifiedByID != null ? (object)entity.ModifiedByID : DBNull.Value);   cmd.Parameters.Add(pModifiedByID); 
        
            return cmd;
        }

        protected Comment CommentFromRow(DataRow row)
        {
            var entity = new Comment();

                    entity.ID = (System.Int64?)row["ID"];
                    entity.Text = (System.String)row["Text"];
                    entity.CreatedDate = (System.DateTime)row["CreatedDate"];
                    entity.CreatedByID = (System.Int64)row["CreatedByID"];
                    entity.ModifiedDate = (System.DateTime?)row["ModifiedDate"];
                    entity.ModifiedByID = (System.Int64?)row["ModifiedByID"];
        
            return entity;
        }
        
    }
}
