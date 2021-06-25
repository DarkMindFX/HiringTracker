

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRT.Interfaces.Entities;

namespace HRT.Interfaces
{
    public interface IPositionCommentDal : IDalBase<PositionComment>
    {
        PositionComment Get(System.Int64 PositionID,System.Int64 CommentID);

        bool Delete(System.Int64 PositionID,System.Int64 CommentID);

        IList<PositionComment> GetByPositionID(System.Int64 PositionID);
        IList<PositionComment> GetByCommentID(System.Int64 CommentID);
            }
}

