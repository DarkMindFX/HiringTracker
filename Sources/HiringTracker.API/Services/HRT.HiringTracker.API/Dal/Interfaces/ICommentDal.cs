

using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.HiringTracker.API.Dal
{
    public interface ICommentDal : IDalBase<Comment>
    {
        Comment Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<Comment> GetByCreatedByID(System.Int64 CreatedByID);
            IList<Comment> GetByModifiedByID(System.Int64? ModifiedByID);
        }
}
