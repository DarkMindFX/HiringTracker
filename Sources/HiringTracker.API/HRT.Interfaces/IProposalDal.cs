using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRT.Interfaces.Entities;

namespace HRT.Interfaces
{
    public interface IProposalDal : IDalBase<Proposal>
    {
        IList<Proposal> GetByCandidate(long id);

        IList<Proposal> GetByPosition(long id);
    }
}
