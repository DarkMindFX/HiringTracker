

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
    class ProposalStatusDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IProposalStatusDal))]
    public class ProposalStatusDal: SQLDal, IProposalStatusDal
    {
        public IInitParams CreateInitParams()
        {
            return new ProposalStatusDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<ProposalStatus>("p_ProposalStatus_Delete", id, "@ID");

            return removed;
        }

        public ProposalStatus Get(long id)
        {
            ProposalStatus entityOut = base.Get<ProposalStatus>("p_ProposalStatus_GetDetails", id, "@ID", ProposalStatusFromRow);

            return entityOut;
        }

        
        public IList<ProposalStatus> GetAll()
        {
            IList<ProposalStatus> result = base.GetAll<ProposalStatus>("p_ProposalStatus_GetAll", ProposalStatusFromRow);

            return result;
        }

        public ProposalStatus Upsert(ProposalStatus entity) 
        {
            ProposalStatus entityOut = base.Upsert<ProposalStatus>("p_ProposalStatus_Upsert", entity, AddUpsertParameters, ProposalStatusFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, ProposalStatus entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pName = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, (object)entity.Name != null ? (object)entity.Name : DBNull.Value);   cmd.Parameters.Add(pName); 
        
            return cmd;
        }

        protected ProposalStatus ProposalStatusFromRow(DataRow row)
        {
            var entity = new ProposalStatus();

                    entity.ID = (System.Int64?)row["ID"];
                    entity.Name = (System.String)row["Name"];
        
            return entity;
        }
        
    }
}
