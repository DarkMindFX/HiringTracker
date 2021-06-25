

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRT.Interfaces.Entities;

namespace HRT.Interfaces
{
    public interface IInterviewDal : IDalBase<Interview>
    {
        Interview Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        IList<Interview> GetByProposalID(System.Int64 ProposalID);
        IList<Interview> GetByInterviewTypeID(System.Int64 InterviewTypeID);
        IList<Interview> GetByInterviewStatusID(System.Int64 InterviewStatusID);
        IList<Interview> GetByCreatedByID(System.Int64 CreatedByID);
        IList<Interview> GetByModifiedByID(System.Int64? ModifiedByID);
            }
}

