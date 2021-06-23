

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
    class PositionSkillDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IPositionSkillDal))]
    public class PositionSkillDal: SQLDal, IPositionSkillDal
    {
        public IInitParams CreateInitParams()
        {
            return new PositionSkillDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        PositionSkill Get(System.Int64? ID)
        {
            PositionSkill result = default(PositionSkill);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_PositionSkill_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = PositionSkillFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_PositionSkill_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<PositionSkill> GetByPositionID(System.Int64 PositionID)
        {
            var entitiesOut = base.GetBy<PositionSkill, System.Int64>("p_PositionSkill_GetByPositionID", PositionID, "@PositionID", SqlDbType.BigInt, 0, PositionSkillFromRow);

            return entitiesOut;
        }
                public IList<PositionSkill> GetBySkillID(System.Int64 SkillID)
        {
            var entitiesOut = base.GetBy<PositionSkill, System.Int64>("p_PositionSkill_GetBySkillID", SkillID, "@SkillID", SqlDbType.BigInt, 0, PositionSkillFromRow);

            return entitiesOut;
        }
                public IList<PositionSkill> GetBySkillProficiencyID(System.Int64 SkillProficiencyID)
        {
            var entitiesOut = base.GetBy<PositionSkill, System.Int64>("p_PositionSkill_GetBySkillProficiencyID", SkillProficiencyID, "@SkillProficiencyID", SqlDbType.BigInt, 0, PositionSkillFromRow);

            return entitiesOut;
        }
        
        public IList<PositionSkill> GetAll()
        {
            IList<PositionSkill> result = base.GetAll<PositionSkill>("p_PositionSkill_GetAll", PositionSkillFromRow);

            return result;
        }

        public PositionSkill Upsert(PositionSkill entity) 
        {
            PositionSkill entityOut = base.Upsert<PositionSkill>("p_PositionSkill_Upsert", entity, AddUpsertParameters, PositionSkillFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, PositionSkill entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pPositionID = new SqlParameter("@PositionID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "PositionID", DataRowVersion.Current, (object)entity.PositionID != null ? (object)entity.PositionID : DBNull.Value);   cmd.Parameters.Add(pPositionID); 
                SqlParameter pSkillID = new SqlParameter("@SkillID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "SkillID", DataRowVersion.Current, (object)entity.SkillID != null ? (object)entity.SkillID : DBNull.Value);   cmd.Parameters.Add(pSkillID); 
                SqlParameter pIsMandatory = new SqlParameter("@IsMandatory", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsMandatory", DataRowVersion.Current, (object)entity.IsMandatory != null ? (object)entity.IsMandatory : DBNull.Value);   cmd.Parameters.Add(pIsMandatory); 
                SqlParameter pSkillProficiencyID = new SqlParameter("@SkillProficiencyID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "SkillProficiencyID", DataRowVersion.Current, (object)entity.SkillProficiencyID != null ? (object)entity.SkillProficiencyID : DBNull.Value);   cmd.Parameters.Add(pSkillProficiencyID); 
        
            return cmd;
        }

        protected PositionSkill PositionSkillFromRow(DataRow row)
        {
            var entity = new PositionSkill();

                    entity.ID = (System.Int64?)row["ID"];
                    entity.PositionID = (System.Int64)row["PositionID"];
                    entity.SkillID = (System.Int64)row["SkillID"];
                    entity.IsMandatory = (System.Boolean)row["IsMandatory"];
                    entity.SkillProficiencyID = (System.Int64)row["SkillProficiencyID"];
        
            return entity;
        }
        
    }
}
