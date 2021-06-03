using HRT.Common;
using HRT.Interfaces;
using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Text;

namespace HRT.DAL.MSSQL
{
    class PositionCandidateStepDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IPositionCandidateStepDal))]
    public class PositionCandidateStepDal : SQLDal, IPositionCandidateStepDal
    {
        public IInitParams CreateInitParams()
        {
            return new PositionCandidateStepDalInitParams();
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public PositionCandidateStep Get(long id)
        {
            throw new NotImplementedException();
        }

        public IList<PositionCandidateStep> GetAll()
        {
            IList<PositionCandidateStep> result = base.GetAll<PositionCandidateStep>("p_PositionCandidateStep_GetAll", PositionCandidateStepFromRow);

            return result;
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public long? Upsert(PositionCandidateStep entity, long? editorID)
        {
            throw new NotImplementedException();
        }

        public PositionCandidateStep Upsert(PositionCandidateStep entity)
        {
            throw new NotImplementedException();
        }

        #region Support method
        private PositionCandidateStep PositionCandidateStepFromRow(DataRow row)
        {
            var entity = new PositionCandidateStep();

            entity.Name = (string)row["Name"];
            entity.StepID = (long)row["StepID"];
            entity.ReqDueDate = (bool)row["ReqDueDate"];
            entity.RequiresRespInDays = (int)row["RequiresRespInDays"];

            return entity;
        }
        #endregion
    }
}
