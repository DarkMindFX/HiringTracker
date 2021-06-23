

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

        public bool Delete(long id)
        {
            bool removed = base.Delete<PositionStatus>("p_PositionStatus_Delete", id, "@ID");

            return removed;
        }

        public PositionStatus Get(long id)
        {
            PositionStatus entityOut = base.Get<PositionStatus>("p_PositionStatus_GetDetails", id, "@ID", PositionStatusFromRow);

            return entityOut;
        }

        
        public IList<PositionStatus> GetAll()
        {
            IList<PositionStatus> result = base.GetAll<PositionStatus>("p_PositionStatus_GetAll", PositionStatusFromRow);

            return result;
        }

        public PositionStatus Upsert(PositionStatus entity) 
        {
            PositionStatus entityOut = base.Upsert<PositionStatus>("p_PositionStatus_Upsert", entity, AddUpsertParameters, PositionStatusFromRow);

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

                    entity.ID = (System.Int64?)row["ID"];
                    entity.Name = (System.String)row["Name"];
        
            return entity;
        }
        
    }
}
