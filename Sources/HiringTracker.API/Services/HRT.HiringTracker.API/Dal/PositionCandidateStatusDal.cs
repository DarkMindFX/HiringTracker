using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IProposalStatusDal))]
    public class ProposalStatusDal : DalBaseImpl<ProposalStatus, Interfaces.IProposalStatusDal>, IProposalStatusDal
    {

        public ProposalStatusDal(Interfaces.IProposalStatusDal dalImpl) : base(dalImpl)
        {
        }

        public new IDictionary<long, ProposalStatus> GetAllAsDictionary()
        {
            var entites = _dalImpl.GetAll();

            IDictionary<long, ProposalStatus> result = entites.ToDictionary(s => (long)s.ID);

            return result;
        }
    }
}
