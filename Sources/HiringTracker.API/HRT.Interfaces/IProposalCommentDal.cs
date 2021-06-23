

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
                IList<ProposalComment> GetByProposalID(System.Int64 ProposalID);
                IList<ProposalComment> GetByCommentID(System.Int64 CommentID);
            }
}

