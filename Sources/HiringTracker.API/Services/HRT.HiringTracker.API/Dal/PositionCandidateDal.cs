using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRT.HiringTracker.API.Dal
{
    public class PositionCandidateDal : DalBaseImpl<PositionCandidate, Interfaces.IPositionCandidateDal>, IPositionCandidateDal
    {
        public PositionCandidateDal(Interfaces.IPositionCandidateDal dalImpl) : base(dalImpl)
        {
        }

        public IList<PositionCandidate> GetByCandidate(long id)
        {
            return _dalImpl.GetByCandidate(id);
        }

        public IList<PositionCandidate> GetByPosition(long id)
        {
            return _dalImpl.GetByPosition(id);
        }
    }
}
