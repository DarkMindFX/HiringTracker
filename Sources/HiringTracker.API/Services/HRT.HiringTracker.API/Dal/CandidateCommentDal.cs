

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(ICandidateCommentDal))]
    public class CandidateCommentDal : DalBaseImpl<CandidateComment, Interfaces.ICandidateCommentDal>, ICandidateCommentDal
    {

        public CandidateCommentDal(Interfaces.ICandidateCommentDal dalImpl) : base(dalImpl)
        {
        }

        public CandidateComment Get(System.Int64 CandidateID,System.Int64 CommentID)
        {
            return _dalImpl.Get(            CandidateID,            CommentID);
        }

        public bool Delete(System.Int64 CandidateID,System.Int64 CommentID)
        {
            return _dalImpl.Delete(            CandidateID,            CommentID);
        }

        public IList<CandidateComment> GetByCandidateID(System.Int64 CandidateID)
        {
            return _dalImpl.GetByCandidateID(CandidateID);
        }
        public IList<CandidateComment> GetByCommentID(System.Int64 CommentID)
        {
            return _dalImpl.GetByCommentID(CommentID);
        }
            }
}
