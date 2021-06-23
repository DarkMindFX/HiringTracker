

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRT.Interfaces.Entities;

namespace HRT.Interfaces
{
    public interface ICommentDal : IDalBase<Comment>
    {
                IList<Comment> GetByCreatedByID(System.Int64 CreatedByID);
                IList<Comment> GetByModifiedByID(System.Int64? ModifiedByID);
            }
}

