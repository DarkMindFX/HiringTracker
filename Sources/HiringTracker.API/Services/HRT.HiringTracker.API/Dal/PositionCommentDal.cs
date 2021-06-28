

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IPositionCommentDal))]
    public class PositionCommentDal : DalBaseImpl<PositionComment, Interfaces.IPositionCommentDal>, IPositionCommentDal
    {

        public PositionCommentDal(Interfaces.IPositionCommentDal dalImpl) : base(dalImpl)
        {
        }

        public PositionComment Get(System.Int64 PositionID,System.Int64 CommentID)
        {
            return _dalImpl.Get(            PositionID,            CommentID);
        }

        public bool Delete(System.Int64 PositionID,System.Int64 CommentID)
        {
            return _dalImpl.Delete(            PositionID,            CommentID);
        }

        public IList<PositionComment> GetByPositionID(System.Int64 PositionID)
        {
            return _dalImpl.GetByPositionID(PositionID);
        }
        public IList<PositionComment> GetByCommentID(System.Int64 CommentID)
        {
            return _dalImpl.GetByCommentID(CommentID);
        }
            }
}
