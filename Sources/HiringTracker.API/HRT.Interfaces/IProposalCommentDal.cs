

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRT.Interfaces.Entities;

namespace HRT.Interfaces
{
    public interface IProposalCommentDal : IDalBase<ProposalComment>
    {
        ProposalComment Get(System.Int64 ProposalID,System.Int64 CommentID);

        bool Delete(System.Int64 ProposalID,System.Int64 CommentID);

        IList<ProposalComment> GetByProposalID(System.Int64 ProposalID);
        IList<ProposalComment> GetByCommentID(System.Int64 CommentID);
            }
}

