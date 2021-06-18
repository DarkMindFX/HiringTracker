using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IPositionStatusDal))]
    public class PositionStatusDal : DalBaseImpl<PositionStatus, Interfaces.IPositionStatusDal>, IPositionStatusDal
    {

        public PositionStatusDal(Interfaces.IPositionStatusDal dalImpl) : base(dalImpl)
        {
        }

        public new IDictionary<long, PositionStatus> GetAllAsDictionary()
        {
            var entites = _dalImpl.GetAll();

            IDictionary<long, PositionStatus> result = entites.ToDictionary(s => (long)s.ID);

            return result;
        }
    }
}
