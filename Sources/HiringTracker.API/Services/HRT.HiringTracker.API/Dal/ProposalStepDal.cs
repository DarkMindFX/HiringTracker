

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IProposalStepDal))]
    public class ProposalStepDal : DalBaseImpl<ProposalStep, Interfaces.IProposalStepDal>, IProposalStepDal
    {

        public ProposalStepDal(Interfaces.IProposalStepDal dalImpl) : base(dalImpl)
        {
        }

        public ProposalStep Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

            }
}
