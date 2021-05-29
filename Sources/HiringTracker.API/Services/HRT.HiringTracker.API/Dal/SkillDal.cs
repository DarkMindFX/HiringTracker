using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;

namespace HRT.HiringTracker.API.Dal
{

    [Export(typeof(ISkillDal))]
    public class SkillDal : DalBaseImpl<Skill, Interfaces.ISkillDal>, ISkillDal
    {

        public SkillDal(Interfaces.ISkillDal dalImpl) : base(dalImpl)
        {
        }

        public new IDictionary<long, Skill> GetAllAsDictionary()
        {
            var statuses = _dalImpl.GetAll();

            IDictionary<long, Skill> result = statuses.ToDictionary(s => s.SkillID);

            return result;
        }
    }
}
