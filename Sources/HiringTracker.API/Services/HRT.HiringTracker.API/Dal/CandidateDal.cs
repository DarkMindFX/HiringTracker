using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(ICandidateDal))]
    public class CandidateDal : DalBaseImpl<Candidate, Interfaces.ICandidateDal>, ICandidateDal
    {

        public CandidateDal(Interfaces.ICandidateDal dalImpl) : base(dalImpl)
        {
        }

        public IList<CandidateSkill> GetSkills(long id)
        {
            return _dalImpl.GetSkills(id);
        }

        public void SetSkills(long id, IList<CandidateSkill> skills)
        {
            _dalImpl.SetSkills(id, skills);
        }

        public new IDictionary<long, Candidate> GetAllAsDictionary()
        {
            var entities = _dalImpl.GetAll();

            IDictionary<long, Candidate> result = entities.ToDictionary(s => s.ID ?? 0);

            return result;
        }
    }
}
