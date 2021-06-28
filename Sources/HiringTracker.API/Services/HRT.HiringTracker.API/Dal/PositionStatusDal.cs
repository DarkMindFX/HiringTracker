

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IPositionStatusDal))]
    public class PositionStatusDal : DalBaseImpl<PositionStatus, Interfaces.IPositionStatusDal>, IPositionStatusDal
    {

        public PositionStatusDal(Interfaces.IPositionStatusDal dalImpl) : base(dalImpl)
        {
        }

        public PositionStatus Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

            }
}
