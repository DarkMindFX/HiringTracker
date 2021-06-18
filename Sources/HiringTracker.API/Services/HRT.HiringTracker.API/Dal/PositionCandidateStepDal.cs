using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IProposalStepDal))]
    public class ProposalStepDal : DalBaseImpl<ProposalStep, Interfaces.IProposalStepDal>, IProposalStepDal
    {

        public ProposalStepDal(Interfaces.IProposalStepDal dalImpl) : base(dalImpl)
        {
        }

        public new IDictionary<long, ProposalStep> GetAllAsDictionary()
        {
            var entites = _dalImpl.GetAll();

            IDictionary<long, ProposalStep> result = entites.ToDictionary(s => (long)s.ID);

            return result;
        }
    }
}
