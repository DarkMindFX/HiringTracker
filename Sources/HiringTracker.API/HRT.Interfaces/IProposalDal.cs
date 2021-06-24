

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
        Proposal Get(
                    System.Int64? ID
        );

        bool Delete(
                    System.Int64? ID
        );

                IList<Proposal> GetByPositionID(System.Int64 PositionID);
                IList<Proposal> GetByCandidateID(System.Int64 CandidateID);
                IList<Proposal> GetByCurrentStepID(System.Int64 CurrentStepID);
                IList<Proposal> GetByNextStepID(System.Int64? NextStepID);
                IList<Proposal> GetByStatusID(System.Int64 StatusID);
                IList<Proposal> GetByCreatedByID(System.Int64? CreatedByID);
                IList<Proposal> GetByModifiedByID(System.Int64? ModifiedByID);
            }
}

