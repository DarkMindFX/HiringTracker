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
    class PositionCandidateStatusDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IPositionCandidateStatusDal))]
    public class PositionCandidateStatusDal : SQLDal, IPositionCandidateStatusDal
    {
        public IInitParams CreateInitParams()
        {
            return new PositionCandidateStatusDalInitParams();
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public PositionCandidateStatus Get(long id)
        {
            throw new NotImplementedException();
        }

        public IList<PositionCandidateStatus> GetAll()
        {
            IList<PositionCandidateStatus> result = base.GetAll<PositionCandidateStatus>("p_PositionCandidateStatus_GetAll", PositionCandidateStatusFromRow);

            return result;
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public long? Upsert(PositionCandidateStatus entity, long? editorID)
        {
            throw new NotImplementedException();
        }

        public PositionCandidateStatus Upsert(PositionCandidateStatus entity)
        {
            throw new NotImplementedException();
        }

        #region Support method
        private PositionCandidateStatus PositionCandidateStatusFromRow(DataRow row)
        {
            var entity = new PositionCandidateStatus();

            entity.Name = (string)row["Name"];
            entity.StatusID = (long)row["StatusID"];

            return entity;
        }
        #endregion
    }
}
