

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
    class PositionStatusDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IPositionStatusDal))]
    public class PositionStatusDal: SQLDal, IPositionStatusDal
    {
        public IInitParams CreateInitParams()
        {
            return new PositionStatusDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public PositionStatus Get(System.Int64? ID)
        {
            PositionStatus result = default(PositionStatus);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_PositionStatus_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = PositionStatusFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_PositionStatus_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

        
        public IList<PositionStatus> GetAll()
        {
            IList<PositionStatus> result = base.GetAll<PositionStatus>("p_PositionStatus_GetAll", PositionStatusFromRow);

            return result;
        }

        public PositionStatus Insert(PositionStatus entity) 
        {
            PositionStatus entityOut = base.Upsert<PositionStatus>("p_PositionStatus_Insert", entity, AddUpsertParameters, PositionStatusFromRow);

            return entityOut;
        }

        public PositionStatus Update(PositionStatus entity) 
        {
            PositionStatus entityOut = base.Upsert<PositionStatus>("p_PositionStatus_Update", entity, AddUpsertParameters, PositionStatusFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, PositionStatus entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pName = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, (object)entity.Name != null ? (object)entity.Name : DBNull.Value);   cmd.Parameters.Add(pName); 
        
            return cmd;
        }

        protected PositionStatus PositionStatusFromRow(DataRow row)
        {
            var entity = new PositionStatus();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.Name = !DBNull.Value.Equals(row["Name"]) ? (System.String)row["Name"] : default(System.String);
        
            return entity;
        }
        
    }
}
