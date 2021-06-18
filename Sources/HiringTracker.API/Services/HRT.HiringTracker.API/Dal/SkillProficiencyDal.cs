using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;

namespace HRT.HiringTracker.API.Dal
{

    [Export(typeof(ISkillProficiencyDal))]
    public class SkillProficiencyDal : DalBaseImpl<SkillProficiency, Interfaces.ISkillProficiencyDal>, ISkillProficiencyDal
    {

        public SkillProficiencyDal(Interfaces.ISkillProficiencyDal dalImpl) : base(dalImpl)
        {
        }

        public new IDictionary<long, SkillProficiency> GetAllAsDictionary()
        {
            var entites = _dalImpl.GetAll();

            IDictionary<long, SkillProficiency> result = entites.ToDictionary(s => (long)s.ID);

            return result;
        }
    }
}
