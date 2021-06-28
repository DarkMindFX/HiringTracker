

using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.HiringTracker.API.Dal
{
    public interface IProposalCommentDal : IDalBase<ProposalComment>
    {
        ProposalComment Get(System.Int64 ProposalID,System.Int64 CommentID);

        bool Delete(System.Int64 ProposalID,System.Int64 CommentID);

            IList<ProposalComment> GetByProposalID(System.Int64 ProposalID);
            IList<ProposalComment> GetByCommentID(System.Int64 CommentID);
        }
}
