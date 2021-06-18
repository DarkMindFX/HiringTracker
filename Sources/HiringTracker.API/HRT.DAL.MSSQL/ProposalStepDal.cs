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
    class ProposalStepDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IProposalStepDal))]
    public class ProposalStepDal: SQLDal, IProposalStepDal
    {
        public IInitParams CreateInitParams()
        {
            return new ProposalStepDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<ProposalStep>("p_ProposalStep_Delete", id, "@ID");

            return removed;
        }

        public ProposalStep Get(long id)
        {
            ProposalStep entityOut = base.Get<ProposalStep>("p_ProposalStep_GetDetails", id, "@ID", ProposalStepFromRow);

            return entityOut;
        }

        public IList<ProposalStep> GetAll()
        {
            IList<ProposalStep> result = base.GetAll<ProposalStep>("p_ProposalStep_GetAll", ProposalStepFromRow);

            return result;
        }

        public ProposalStep Upsert(ProposalStep entity) 
        {
            ProposalStep entityOut = base.Upsert<ProposalStep>("p_ProposalStep_Upsert", entity, AddUpsertParameters, ProposalStepFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, ProposalStep entity)
        {
            		   SqlParameter pID = new SqlParameter(@"ID",    SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 

		   SqlParameter pName = new SqlParameter(@"Name",    SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, (object)entity.Name != null ? (object)entity.Name : DBNull.Value);   cmd.Parameters.Add(pName); 

		   SqlParameter pReqDueDate = new SqlParameter(@"ReqDueDate",    SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "ReqDueDate", DataRowVersion.Current, (object)entity.ReqDueDate != null ? (object)entity.ReqDueDate : DBNull.Value);   cmd.Parameters.Add(pReqDueDate); 

		   SqlParameter pRequiresRespInDays = new SqlParameter(@"RequiresRespInDays",    SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "RequiresRespInDays", DataRowVersion.Current, (object)entity.RequiresRespInDays != null ? (object)entity.RequiresRespInDays : DBNull.Value);   cmd.Parameters.Add(pRequiresRespInDays); 



            return cmd;
        }

        protected ProposalStep ProposalStepFromRow(DataRow row)
        {
            var entity = new ProposalStep();

            		entity.ID = (System.Int64?)row["ID"];
		entity.Name = (System.String)row["Name"];
		entity.ReqDueDate = (System.Boolean)row["ReqDueDate"];
		entity.RequiresRespInDays = !DBNull.Value.Equals(row["RequiresRespInDays"]) ?  (System.Int32?)row["RequiresRespInDays"] : null;


            return entity;
        }

        public long? Upsert(ProposalStep entity, long? editorID)
        {
            throw new NotImplementedException();
        }
    }
}
