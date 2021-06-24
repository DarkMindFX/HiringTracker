

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
        Comment Get(
                    System.Int64? ID
        );

        bool Delete(
                    System.Int64? ID
        );

                IList<Comment> GetByCreatedByID(System.Int64 CreatedByID);
                IList<Comment> GetByModifiedByID(System.Int64? ModifiedByID);
            }
}

