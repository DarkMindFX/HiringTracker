

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(ISkillDal))]
    public class SkillDal : DalBaseImpl<Skill, Interfaces.ISkillDal>, ISkillDal
    {

        public SkillDal(Interfaces.ISkillDal dalImpl) : base(dalImpl)
        {
        }

        public Skill Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

            }
}
