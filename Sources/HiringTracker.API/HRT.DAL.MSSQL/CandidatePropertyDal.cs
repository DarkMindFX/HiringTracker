

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
    class CandidatePropertyDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(ICandidatePropertyDal))]
    public class CandidatePropertyDal: SQLDal, ICandidatePropertyDal
    {
        public IInitParams CreateInitParams()
        {
            return new CandidatePropertyDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<CandidateProperty>("p_CandidateProperty_Delete", id, "@ID");

            return removed;
        }

        public CandidateProperty Get(long id)
        {
            CandidateProperty entityOut = base.Get<CandidateProperty>("p_CandidateProperty_GetDetails", id, "@ID", CandidatePropertyFromRow);

            return entityOut;
        }

                public IList<CandidateProperty> GetByCandidateID(System.Int64 CandidateID)
        {
            var entitiesOut = base.GetBy<CandidateProperty, System.Int64>("p_CandidateProperty_GetByCandidateID", CandidateID, "@CandidateID", SqlDbType.BigInt, 0, CandidatePropertyFromRow);

            return entitiesOut;
        }
        
        public IList<CandidateProperty> GetAll()
        {
            IList<CandidateProperty> result = base.GetAll<CandidateProperty>("p_CandidateProperty_GetAll", CandidatePropertyFromRow);

            return result;
        }

        public CandidateProperty Upsert(CandidateProperty entity) 
        {
            CandidateProperty entityOut = base.Upsert<CandidateProperty>("p_CandidateProperty_Upsert", entity, AddUpsertParameters, CandidatePropertyFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, CandidateProperty entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pName = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, (object)entity.Name != null ? (object)entity.Name : DBNull.Value);   cmd.Parameters.Add(pName); 
                SqlParameter pValue = new SqlParameter("@Value", System.Data.SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Value", DataRowVersion.Current, (object)entity.Value != null ? (object)entity.Value : DBNull.Value);   cmd.Parameters.Add(pValue); 
                SqlParameter pCandidateID = new SqlParameter("@CandidateID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CandidateID", DataRowVersion.Current, (object)entity.CandidateID != null ? (object)entity.CandidateID : DBNull.Value);   cmd.Parameters.Add(pCandidateID); 
        
            return cmd;
        }

        protected CandidateProperty CandidatePropertyFromRow(DataRow row)
        {
            var entity = new CandidateProperty();

                    entity.ID = (System.Int64?)row["ID"];
                    entity.Name = (System.String)row["Name"];
                    entity.Value = (System.String)row["Value"];
                    entity.CandidateID = (System.Int64)row["CandidateID"];
        
            return entity;
        }
        
    }
}
