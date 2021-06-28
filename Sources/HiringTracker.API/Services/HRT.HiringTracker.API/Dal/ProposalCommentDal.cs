

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IProposalCommentDal))]
    public class ProposalCommentDal : DalBaseImpl<ProposalComment, Interfaces.IProposalCommentDal>, IProposalCommentDal
    {

        public ProposalCommentDal(Interfaces.IProposalCommentDal dalImpl) : base(dalImpl)
        {
        }

        public ProposalComment Get(System.Int64 ProposalID,System.Int64 CommentID)
        {
            return _dalImpl.Get(            ProposalID,            CommentID);
        }

        public bool Delete(System.Int64 ProposalID,System.Int64 CommentID)
        {
            return _dalImpl.Delete(            ProposalID,            CommentID);
        }

        public IList<ProposalComment> GetByProposalID(System.Int64 ProposalID)
        {
            return _dalImpl.GetByProposalID(ProposalID);
        }
        public IList<ProposalComment> GetByCommentID(System.Int64 CommentID)
        {
            return _dalImpl.GetByCommentID(CommentID);
        }
            }
}
