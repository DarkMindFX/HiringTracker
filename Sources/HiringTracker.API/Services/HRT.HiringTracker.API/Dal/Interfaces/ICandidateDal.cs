using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.HiringTracker.API.Dal
{
    public interface ICandidateDal : IDalBase<Candidate>
    {
        IList<CandidateSkill> GetSkills(long id);

        void SetSkills(long id, IList<CandidateSkill> skills);
    }
}
