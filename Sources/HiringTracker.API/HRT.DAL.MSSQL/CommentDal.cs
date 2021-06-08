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

        public bool Delete(long id)
        {
            bool removed = base.Delete<Comment>("p_Comment_Delete", id, "@ID");

            return removed;
        }

        public Comment Get(long id)
        {
            Comment entityOut = base.Get<Comment>("p_Comment_GetDetails", id, "@ID", CommentFromRow);

            return entityOut;
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
            		   SqlParameter pID = new SqlParameter(@"ID",    SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 

		   SqlParameter pText = new SqlParameter(@"Text",    SqlDbType.NVarChar, 0, ParameterDirection.Input, false, 0, 0, "Text", DataRowVersion.Current, (object)entity.Text != null ? (object)entity.Text : DBNull.Value);   cmd.Parameters.Add(pText); 

		   SqlParameter pCreatedDate = new SqlParameter(@"CreatedDate",    SqlDbType.DateTime2, 0, ParameterDirection.Input, false, 0, 0, "CreatedDate", DataRowVersion.Current, (object)entity.CreatedDate != null ? (object)entity.CreatedDate : DBNull.Value);   cmd.Parameters.Add(pCreatedDate); 

		   SqlParameter pCreatedByID = new SqlParameter(@"CreatedByID",    SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CreatedByID", DataRowVersion.Current, (object)entity.CreatedByID != null ? (object)entity.CreatedByID : DBNull.Value);   cmd.Parameters.Add(pCreatedByID); 

		   SqlParameter pModifiedDate = new SqlParameter(@"ModifiedDate",    SqlDbType.DateTime2, 0, ParameterDirection.Input, true, 0, 0, "ModifiedDate", DataRowVersion.Current, (object)entity.ModifiedDate != null ? (object)entity.ModifiedDate : DBNull.Value);   cmd.Parameters.Add(pModifiedDate); 

		   SqlParameter pModifiedByID = new SqlParameter(@"ModifiedByID",    SqlDbType.BigInt, 0, ParameterDirection.Input, true, 0, 0, "ModifiedByID", DataRowVersion.Current, (object)entity.ModifiedByID != null ? (object)entity.ModifiedByID : DBNull.Value);   cmd.Parameters.Add(pModifiedByID); 



            return cmd;
        }

        protected Comment CommentFromRow(DataRow row)
        {
            var entity = new Comment();

            		entity.ID = (System.Int64?)row["ID"];
		entity.Text = (System.String)row["Text"];
		entity.CreatedDate = (System.DateTime)row["CreatedDate"];
		entity.CreatedByID = (System.Int64)row["CreatedByID"];
		entity.ModifiedDate = !DBNull.Value.Equals(row["ModifiedDate"]) ?  (System.DateTime?)row["ModifiedDate"] : null;
		entity.ModifiedByID = !DBNull.Value.Equals(row["ModifiedByID"]) ?  (System.Int64?)row["ModifiedByID"] : null;


            return entity;
        }

        public long? Upsert(Comment entity, long? editorID)
        {
            throw new NotImplementedException();
        }
    }
}
