

using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.HiringTracker.API.Dal
{
    public interface IPositionCommentDal : IDalBase<PositionComment>
    {
        PositionComment Get(System.Int64 PositionID,System.Int64 CommentID);

        bool Delete(System.Int64 PositionID,System.Int64 CommentID);

            IList<PositionComment> GetByPositionID(System.Int64 PositionID);
            IList<PositionComment> GetByCommentID(System.Int64 CommentID);
        }
}
