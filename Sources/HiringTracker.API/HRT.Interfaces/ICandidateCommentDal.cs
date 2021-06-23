

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRT.Interfaces.Entities;

namespace HRT.Interfaces
{
    public interface ICandidateCommentDal : IDalBase<CandidateComment>
    {
                IList<CandidateComment> GetByCandidateID(System.Int64 CandidateID);
                IList<CandidateComment> GetByCommentID(System.Int64 CommentID);
            }
}

