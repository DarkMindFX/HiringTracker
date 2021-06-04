using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.HiringTracker.API.Dal
{
    public interface IPositionCandidateDal :  IDalBase<PositionCandidate>
    {
        IList<PositionCandidate> GetByPosition(long id);

        IList<PositionCandidate> GetByCandidate(long id);
    }
}
