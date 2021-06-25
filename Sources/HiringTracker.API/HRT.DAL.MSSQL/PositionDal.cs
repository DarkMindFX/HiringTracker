

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
    class PositionDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IPositionDal))]
    public class PositionDal: SQLDal, IPositionDal
    {
        public IInitParams CreateInitParams()
        {
            return new PositionDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public Position Get(System.Int64? ID)
        {
            Position result = default(Position);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Position_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = PositionFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Position_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<Position> GetByDepartmentID(System.Int64? DepartmentID)
        {
            var entitiesOut = base.GetBy<Position, System.Int64?>("p_Position_GetByDepartmentID", DepartmentID, "@DepartmentID", SqlDbType.BigInt, 0, PositionFromRow);

            return entitiesOut;
        }
                public IList<Position> GetByStatusID(System.Int64 StatusID)
        {
            var entitiesOut = base.GetBy<Position, System.Int64>("p_Position_GetByStatusID", StatusID, "@StatusID", SqlDbType.BigInt, 0, PositionFromRow);

            return entitiesOut;
        }
                public IList<Position> GetByCreatedByID(System.Int64 CreatedByID)
        {
            var entitiesOut = base.GetBy<Position, System.Int64>("p_Position_GetByCreatedByID", CreatedByID, "@CreatedByID", SqlDbType.BigInt, 0, PositionFromRow);

            return entitiesOut;
        }
                public IList<Position> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            var entitiesOut = base.GetBy<Position, System.Int64?>("p_Position_GetByModifiedByID", ModifiedByID, "@ModifiedByID", SqlDbType.BigInt, 0, PositionFromRow);

            return entitiesOut;
        }
        
        public IList<Position> GetAll()
        {
            IList<Position> result = base.GetAll<Position>("p_Position_GetAll", PositionFromRow);

            return result;
        }

        public Position Insert(Position entity) 
        {
            Position entityOut = base.Upsert<Position>("p_Position_Insert", entity, AddUpsertParameters, PositionFromRow);

            return entityOut;
        }

        public Position Update(Position entity) 
        {
            Position entityOut = base.Upsert<Position>("p_Position_Update", entity, AddUpsertParameters, PositionFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Position entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pDepartmentID = new SqlParameter("@DepartmentID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "DepartmentID", DataRowVersion.Current, (object)entity.DepartmentID != null ? (object)entity.DepartmentID : DBNull.Value);   cmd.Parameters.Add(pDepartmentID); 
                SqlParameter pTitle = new SqlParameter("@Title", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Title", DataRowVersion.Current, (object)entity.Title != null ? (object)entity.Title : DBNull.Value);   cmd.Parameters.Add(pTitle); 
                SqlParameter pShortDesc = new SqlParameter("@ShortDesc", System.Data.SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, "ShortDesc", DataRowVersion.Current, (object)entity.ShortDesc != null ? (object)entity.ShortDesc : DBNull.Value);   cmd.Parameters.Add(pShortDesc); 
                SqlParameter pDescription = new SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 4000, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Current, (object)entity.Description != null ? (object)entity.Description : DBNull.Value);   cmd.Parameters.Add(pDescription); 
                SqlParameter pStatusID = new SqlParameter("@StatusID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "StatusID", DataRowVersion.Current, (object)entity.StatusID != null ? (object)entity.StatusID : DBNull.Value);   cmd.Parameters.Add(pStatusID); 
                SqlParameter pCreatedDate = new SqlParameter("@CreatedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CreatedDate", DataRowVersion.Current, (object)entity.CreatedDate != null ? (object)entity.CreatedDate : DBNull.Value);   cmd.Parameters.Add(pCreatedDate); 
                SqlParameter pCreatedByID = new SqlParameter("@CreatedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CreatedByID", DataRowVersion.Current, (object)entity.CreatedByID != null ? (object)entity.CreatedByID : DBNull.Value);   cmd.Parameters.Add(pCreatedByID); 
                SqlParameter pModifiedDate = new SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ModifiedDate", DataRowVersion.Current, (object)entity.ModifiedDate != null ? (object)entity.ModifiedDate : DBNull.Value);   cmd.Parameters.Add(pModifiedDate); 
                SqlParameter pModifiedByID = new SqlParameter("@ModifiedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ModifiedByID", DataRowVersion.Current, (object)entity.ModifiedByID != null ? (object)entity.ModifiedByID : DBNull.Value);   cmd.Parameters.Add(pModifiedByID); 
        
            return cmd;
        }

        protected Position PositionFromRow(DataRow row)
        {
            var entity = new Position();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.DepartmentID = !DBNull.Value.Equals(row["DepartmentID"]) ? (System.Int64?)row["DepartmentID"] : default(System.Int64?);
                    entity.Title = !DBNull.Value.Equals(row["Title"]) ? (System.String)row["Title"] : default(System.String);
                    entity.ShortDesc = !DBNull.Value.Equals(row["ShortDesc"]) ? (System.String)row["ShortDesc"] : default(System.String);
                    entity.Description = !DBNull.Value.Equals(row["Description"]) ? (System.String)row["Description"] : default(System.String);
                    entity.StatusID = !DBNull.Value.Equals(row["StatusID"]) ? (System.Int64)row["StatusID"] : default(System.Int64);
                    entity.CreatedDate = !DBNull.Value.Equals(row["CreatedDate"]) ? (System.DateTime)row["CreatedDate"] : default(System.DateTime);
                    entity.CreatedByID = !DBNull.Value.Equals(row["CreatedByID"]) ? (System.Int64)row["CreatedByID"] : default(System.Int64);
                    entity.ModifiedDate = !DBNull.Value.Equals(row["ModifiedDate"]) ? (System.DateTime?)row["ModifiedDate"] : default(System.DateTime?);
                    entity.ModifiedByID = !DBNull.Value.Equals(row["ModifiedByID"]) ? (System.Int64?)row["ModifiedByID"] : default(System.Int64?);
        
            return entity;
        }
        
    }
}
