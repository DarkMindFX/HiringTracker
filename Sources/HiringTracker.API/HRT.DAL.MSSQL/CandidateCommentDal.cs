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
    public class CandidateCommentDal : SQLDal, ICandidateCommentDal
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
            throw new NotSupportedException("Delete by ID is not supported");
        }

        public bool Delete(long candidateId, long commentId)
        {
            bool result = false;

            string procName = "p_CandidateComment_Delete";

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand(procName, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter pCandidateID = new SqlParameter(@"CandidateID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CandidateID", DataRowVersion.Current, candidateId); cmd.Parameters.Add(pCandidateID);

                SqlParameter pCommentID = new SqlParameter(@"CommentID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CommentID", DataRowVersion.Current, commentId); cmd.Parameters.Add(pCommentID);

                var pRemoved = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pRemoved.Value;
            }

            return result;
        }

        public CandidateComment Get(long id)
        {
            CandidateComment entityOut = base.Get<CandidateComment>("p_CandidateComment_GetDetails", id, "@ID", CandidateCommentFromRow);

            return entityOut;
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
            SqlParameter pCandidateID = new SqlParameter(@"CandidateID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CandidateID", DataRowVersion.Current, (object)entity.CandidateID != null ? (object)entity.CandidateID : DBNull.Value); cmd.Parameters.Add(pCandidateID);

            SqlParameter pCommentID = new SqlParameter(@"CommentID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CommentID", DataRowVersion.Current, (object)entity.CommentID != null ? (object)entity.CommentID : DBNull.Value); cmd.Parameters.Add(pCommentID);

            return cmd;
        }

        protected CandidateComment CandidateCommentFromRow(DataRow row)
        {
            var entity = new CandidateComment();

            entity.CandidateID = (System.Int64)row["CandidateID"];
            entity.CommentID = (System.Int64)row["CommentID"];


            return entity;
        }

        public long? Upsert(CandidateComment entity, long? editorID)
        {
            throw new NotImplementedException();
        }

        public IList<CandidateComment> GetByCandidate(long candidateId)
        {
            IList<CandidateComment> result = null;

            string procName = "p_CandidateComment_Delete";

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand(procName, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter pCandidateID = new SqlParameter(@"CandidateID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CandidateID", DataRowVersion.Current, candidateId); cmd.Parameters.Add(pCandidateID);

                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                DataSet ds = FillDataSet(cmd);

                if((bool)pFound.Value)
                {
                    result = new List<CandidateComment>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var c = CandidateCommentFromRow(row);

                        result.Add(c);
                    }
                }
            }

            return result;
        }
    }
}
