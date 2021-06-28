

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(ISkillProficiencyDal))]
    public class SkillProficiencyDal : DalBaseImpl<SkillProficiency, Interfaces.ISkillProficiencyDal>, ISkillProficiencyDal
    {

        public SkillProficiencyDal(Interfaces.ISkillProficiencyDal dalImpl) : base(dalImpl)
        {
        }

        public SkillProficiency Get(System.Int64 ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64 ID)
        {
            return _dalImpl.Delete(            ID);
        }

            }
}
