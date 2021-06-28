

using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.HiringTracker.API.Dal
{
    public interface IProposalStatusDal : IDalBase<ProposalStatus>
    {
        ProposalStatus Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        }
}
