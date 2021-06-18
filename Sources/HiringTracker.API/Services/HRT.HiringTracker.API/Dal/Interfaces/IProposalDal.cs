using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.HiringTracker.API.Dal
{
    public interface IProposalDal : IDalBase<Proposal>
    {
        IList<Proposal> GetByCandidate(long id);

        IList<Proposal> GetByPosition(long id);
    }
}
