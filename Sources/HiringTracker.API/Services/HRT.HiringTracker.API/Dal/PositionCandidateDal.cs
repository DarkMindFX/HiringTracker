using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRT.HiringTracker.API.Dal
{
    public class ProposalDal : DalBaseImpl<Proposal, Interfaces.IProposalDal>, IProposalDal
    {
        public ProposalDal(Interfaces.IProposalDal dalImpl) : base(dalImpl)
        {
        }

        public IList<Proposal> GetByCandidate(long id)
        {
            return _dalImpl.GetByCandidate(id);
        }

        public IList<Proposal> GetByPosition(long id)
        {
            return _dalImpl.GetByPosition(id);
        }
    }
}
