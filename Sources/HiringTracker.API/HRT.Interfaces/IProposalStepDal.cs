

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRT.Interfaces.Entities;

namespace HRT.Interfaces
{
    public interface IProposalStepDal : IDalBase<ProposalStep>
    {
        ProposalStep Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            }
}

