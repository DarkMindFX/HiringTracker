

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IProposalStatusDal))]
    public class ProposalStatusDal : DalBaseImpl<ProposalStatus, Interfaces.IProposalStatusDal>, IProposalStatusDal
    {

        public ProposalStatusDal(Interfaces.IProposalStatusDal dalImpl) : base(dalImpl)
        {
        }

        public ProposalStatus Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

            }
}
