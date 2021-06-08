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
    class SkillDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(ISkillDal))]
    public class SkillDal: SQLDal, ISkillDal
    {
        public IInitParams CreateInitParams()
        {
            return new SkillDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<Skill>("p_Skill_Delete", id, "@ID");

            return removed;
        }

        public Skill Get(long id)
        {
            Skill entityOut = base.Get<Skill>("p_Skill_GetDetails", id, "@ID", SkillFromRow);

            return entityOut;
        }

        public IList<Skill> GetAll()
        {
            IList<Skill> result = base.GetAll<Skill>("p_Skill_GetAll", SkillFromRow);

            return result;
        }

        public Skill Upsert(Skill entity) 
        {
            Skill entityOut = base.Upsert<Skill>("p_Skill_Upsert", entity, AddUpsertParameters, SkillFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Skill entity)
        {
            		   SqlParameter pID = new SqlParameter(@"ID",    SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 

		   SqlParameter pName = new SqlParameter(@"Name",    SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, (object)entity.Name != null ? (object)entity.Name : DBNull.Value);   cmd.Parameters.Add(pName); 



            return cmd;
        }

        protected Skill SkillFromRow(DataRow row)
        {
            var entity = new Skill();

            		entity.ID = (System.Int64?)row["ID"];
		entity.Name = (System.String)row["Name"];


            return entity;
        }

        public long? Upsert(Skill entity, long? editorID)
        {
            throw new NotImplementedException();
        }
    }
}
