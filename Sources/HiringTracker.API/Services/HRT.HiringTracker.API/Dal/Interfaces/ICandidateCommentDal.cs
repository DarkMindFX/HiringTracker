

using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.HiringTracker.API.Dal
{
    public interface ICandidateCommentDal : IDalBase<CandidateComment>
    {
        CandidateComment Get(System.Int64 CandidateID,System.Int64 CommentID);

        bool Delete(System.Int64 CandidateID,System.Int64 CommentID);

            IList<CandidateComment> GetByCandidateID(System.Int64 CandidateID);
            IList<CandidateComment> GetByCommentID(System.Int64 CommentID);
        }
}
