

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
    class SkillProficiencyDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(ISkillProficiencyDal))]
    public class SkillProficiencyDal: SQLDal, ISkillProficiencyDal
    {
        public IInitParams CreateInitParams()
        {
            return new SkillProficiencyDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public SkillProficiency Get(System.Int64 ID)
        {
            SkillProficiency result = default(SkillProficiency);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_SkillProficiency_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = SkillProficiencyFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64 ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_SkillProficiency_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

        
        public IList<SkillProficiency> GetAll()
        {
            IList<SkillProficiency> result = base.GetAll<SkillProficiency>("p_SkillProficiency_GetAll", SkillProficiencyFromRow);

            return result;
        }

        public SkillProficiency Insert(SkillProficiency entity) 
        {
            SkillProficiency entityOut = base.Upsert<SkillProficiency>("p_SkillProficiency_Insert", entity, AddUpsertParameters, SkillProficiencyFromRow);

            return entityOut;
        }

        public SkillProficiency Update(SkillProficiency entity) 
        {
            SkillProficiency entityOut = base.Upsert<SkillProficiency>("p_SkillProficiency_Update", entity, AddUpsertParameters, SkillProficiencyFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, SkillProficiency entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pName = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, (object)entity.Name != null ? (object)entity.Name : DBNull.Value);   cmd.Parameters.Add(pName); 
        
            return cmd;
        }

        protected SkillProficiency SkillProficiencyFromRow(DataRow row)
        {
            var entity = new SkillProficiency();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64)row["ID"] : default(System.Int64);
                    entity.Name = !DBNull.Value.Equals(row["Name"]) ? (System.String)row["Name"] : default(System.String);
        
            return entity;
        }
        
    }
}
