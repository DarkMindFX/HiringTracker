using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IPositionCandidateStatusDal))]
    public class PositionCandidateStatusDal : DalBaseImpl<PositionCandidateStatus, Interfaces.IPositionCandidateStatusDal>, IPositionCandidateStatusDal
    {

        public PositionCandidateStatusDal(Interfaces.IPositionCandidateStatusDal dalImpl) : base(dalImpl)
        {
        }

        public new IDictionary<long, PositionCandidateStatus> GetAllAsDictionary()
        {
            var statuses = _dalImpl.GetAll();

            IDictionary<long, PositionCandidateStatus> result = statuses.ToDictionary(s => s.StatusID);

            return result;
        }
    }
}
