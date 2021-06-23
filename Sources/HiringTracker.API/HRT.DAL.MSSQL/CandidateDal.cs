

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
    class CandidateDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(ICandidateDal))]
    public class CandidateDal : SQLDal, ICandidateDal
    {
        public IInitParams CreateInitParams()
        {
            return new CandidateDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        Candidate Get(System.Int64? ID)
        {
            Candidate result = default(Candidate);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Candidate_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                               ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);


                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = CandidateFromRow(ds.Tables[0].Rows[0]);
                }
            }

            return result;
        }

        bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Candidate_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                    ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);

                var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

        public IList<Candidate> GetByCreatedByID(System.Int64 CreatedByID)
        {
            var entitiesOut = base.GetBy<Candidate, System.Int64>("p_Candidate_GetByCreatedByID", CreatedByID, "@CreatedByID", SqlDbType.BigInt, 0, CandidateFromRow);

            return entitiesOut;
        }
        public IList<Candidate> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            var entitiesOut = base.GetBy<Candidate, System.Int64?>("p_Candidate_GetByModifiedByID", ModifiedByID, "@ModifiedByID", SqlDbType.BigInt, 0, CandidateFromRow);

            return entitiesOut;
        }

        public IList<Candidate> GetAll()
        {
            IList<Candidate> result = base.GetAll<Candidate>("p_Candidate_GetAll", CandidateFromRow);

            return result;
        }

        public Candidate Upsert(Candidate entity)
        {
            Candidate entityOut = base.Upsert<Candidate>("p_Candidate_Upsert", entity, AddUpsertParameters, CandidateFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Candidate entity)
        {
            SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value); cmd.Parameters.Add(pID);
            SqlParameter pFirstName = new SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "FirstName", DataRowVersion.Current, (object)entity.FirstName != null ? (object)entity.FirstName : DBNull.Value); cmd.Parameters.Add(pFirstName);
            SqlParameter pMiddleName = new SqlParameter("@MiddleName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "MiddleName", DataRowVersion.Current, (object)entity.MiddleName != null ? (object)entity.MiddleName : DBNull.Value); cmd.Parameters.Add(pMiddleName);
            SqlParameter pLastName = new SqlParameter("@LastName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "LastName", DataRowVersion.Current, (object)entity.LastName != null ? (object)entity.LastName : DBNull.Value); cmd.Parameters.Add(pLastName);
            SqlParameter pEmail = new SqlParameter("@Email", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Email", DataRowVersion.Current, (object)entity.Email != null ? (object)entity.Email : DBNull.Value); cmd.Parameters.Add(pEmail);
            SqlParameter pPhone = new SqlParameter("@Phone", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Phone", DataRowVersion.Current, (object)entity.Phone != null ? (object)entity.Phone : DBNull.Value); cmd.Parameters.Add(pPhone);
            SqlParameter pCVLink = new SqlParameter("@CVLink", System.Data.SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "CVLink", DataRowVersion.Current, (object)entity.CVLink != null ? (object)entity.CVLink : DBNull.Value); cmd.Parameters.Add(pCVLink);
            SqlParameter pCreatedByID = new SqlParameter("@CreatedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CreatedByID", DataRowVersion.Current, (object)entity.CreatedByID != null ? (object)entity.CreatedByID : DBNull.Value); cmd.Parameters.Add(pCreatedByID);
            SqlParameter pCreatedDate = new SqlParameter("@CreatedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CreatedDate", DataRowVersion.Current, (object)entity.CreatedDate != null ? (object)entity.CreatedDate : DBNull.Value); cmd.Parameters.Add(pCreatedDate);
            SqlParameter pModifiedByID = new SqlParameter("@ModifiedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ModifiedByID", DataRowVersion.Current, (object)entity.ModifiedByID != null ? (object)entity.ModifiedByID : DBNull.Value); cmd.Parameters.Add(pModifiedByID);
            SqlParameter pModifiedDate = new SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ModifiedDate", DataRowVersion.Current, (object)entity.ModifiedDate != null ? (object)entity.ModifiedDate : DBNull.Value); cmd.Parameters.Add(pModifiedDate);

            return cmd;
        }

        protected Candidate CandidateFromRow(DataRow row)
        {
            var entity = new Candidate();

            entity.ID = (System.Int64?)row["ID"];
            entity.FirstName = (System.String)row["FirstName"];
            entity.MiddleName = (System.String)row["MiddleName"];
            entity.LastName = (System.String)row["LastName"];
            entity.Email = (System.String)row["Email"];
            entity.Phone = (System.String)row["Phone"];
            entity.CVLink = (System.String)row["CVLink"];
            entity.CreatedByID = (System.Int64)row["CreatedByID"];
            entity.CreatedDate = (System.DateTime)row["CreatedDate"];
            entity.ModifiedByID = (System.Int64?)row["ModifiedByID"];
            entity.ModifiedDate = (System.DateTime?)row["ModifiedDate"];

            return entity;
        }

    }
}
