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
            IList<SkillProficiency> result = null;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_SkillProficiency_GetAll", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count >= 1)
                {
                    result = new List<SkillProficiency>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var c = SkillProfFromRow(row);

                        result.Add(c);
                    }
                }
            }

            return result;
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public long? Upsert(SkillProficiency entity)
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
