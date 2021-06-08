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
        bool Delete(long candidateId, long commentId);

        IList<CandidateComment> GetByCandidate(long candidateId);
    }
}
