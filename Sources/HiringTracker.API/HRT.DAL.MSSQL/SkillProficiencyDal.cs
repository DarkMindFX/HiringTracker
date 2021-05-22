using HRT.Common;
using HRT.Interfaces;
using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.Text;

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

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public SkillProficiency Get(long id)
        {
            throw new NotImplementedException();
        }

        public IList<SkillProficiency> GetAll()
        {
            IList<SkillProficiency> result = base.GetAll<SkillProficiency>("p_SkillProficiency_GetAll", SkillProfFromRow);
            
            return result;
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public long? Upsert(SkillProficiency entity, long? editorID)
        {
            throw new NotImplementedException();
        }

        #region Support methods
        private SkillProficiency SkillProfFromRow(DataRow row)
        {
            var entity = new SkillProficiency();

            entity.Name = (string)row["Name"];
            entity.ProficiencyID = (long)row["ProficiencyID"];

            return entity;
        }
        #endregion
    }
}
