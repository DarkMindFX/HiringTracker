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
    public class SkillProficiencyDal : SQLDal, ISkillProficiencyDal
    {
        public IInitParams CreateInitParams()
        {
            return new SkillProficiencyDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<SkillProficiency>("p_SkillProficiency_Delete", id, "@ID");

            return removed;
        }

        public SkillProficiency Get(long id)
        {
            SkillProficiency entityOut = base.Get<SkillProficiency>("p_SkillProficiency_GetDetails", id, "@ID", SkillProficiencyFromRow);

            return entityOut;
        }

        public IList<SkillProficiency> GetAll()
        {
            IList<SkillProficiency> result = base.GetAll<SkillProficiency>("p_SkillProficiency_GetAll", SkillProficiencyFromRow);

            return result;
        }

        public SkillProficiency Upsert(SkillProficiency entity)
        {
            SkillProficiency entityOut = base.Upsert<SkillProficiency>("p_SkillProficiency_Upsert", entity, AddUpsertParameters, SkillProficiencyFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, SkillProficiency entity)
        {
            SqlParameter pID = new SqlParameter(@"ID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value); cmd.Parameters.Add(pID);

            SqlParameter pName = new SqlParameter(@"Name", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, (object)entity.Name != null ? (object)entity.Name : DBNull.Value); cmd.Parameters.Add(pName);

            return cmd;
        }

        protected SkillProficiency SkillProficiencyFromRow(DataRow row)
        {
            var entity = new SkillProficiency();

            entity.ID = (System.Int64)row["ID"];
            entity.Name = (System.String)row["Name"];


            return entity;
        }

        public long? Upsert(SkillProficiency entity, long? editorID)
        {
            throw new NotImplementedException();
        }
    }
}
