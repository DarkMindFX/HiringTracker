using HRT.Common;
using HRT.Interfaces;
using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;

namespace HRT.DAL.MSSQL
{
    class SkillDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(ISkillDal))]
    public class SkillDal : SQLDal, ISkillDal
    {
        public IInitParams CreateInitParams()
        {
            return new SkillDalInitParams();
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<Position>("p_Skill_Delete", id, "@SkillID");

            return removed;
        }

        public IList<Skill> GetAll()
        {
            IList<Skill> result = base.GetAll<Skill>("p_Skill_GetAll", SkillFromRow);            

            return result;
        }

        public Skill Get(long id)
        {
            throw new NotImplementedException();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public long? Upsert(Skill entity, long? editorID)
        {
            throw new NotImplementedException();
        }

        #region Support methods
        private Skill SkillFromRow(DataRow row)
        {
            var entity = new Skill();

            entity.Name = (string)row["Name"];
            entity.SkillID = (long)row["SkillID"];

            return entity;
        }
        #endregion
    }
}
