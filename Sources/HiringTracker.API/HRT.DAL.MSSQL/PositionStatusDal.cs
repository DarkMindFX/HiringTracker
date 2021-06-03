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
    class PositionStatusDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IPositionStatusDal))]
    public class PositionStatusDal : SQLDal, IPositionStatusDal
    {
        public IInitParams CreateInitParams()
        {
            return new PositionStatusDalInitParams();
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public PositionStatus Get(long id)
        {
            throw new NotImplementedException();
        }

        public IList<PositionStatus> GetAll()
        {
            IList<PositionStatus> result = base.GetAll<PositionStatus>("p_PositionStatus_GetAll", StatusFromRow);

            return result;
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public long? Upsert(PositionStatus entity, long? editorID)
        {
            throw new NotImplementedException();
        }

        public PositionStatus Upsert(PositionStatus entity)
        {
            throw new NotImplementedException();
        }

        #region Support methods
        private PositionStatus StatusFromRow(DataRow row)
        {
            var entity = new PositionStatus();

            entity.Name = (string)row["Name"];
            entity.StatusID = (long)row["StatusID"];

            return entity;
        }
        #endregion
    }
}
