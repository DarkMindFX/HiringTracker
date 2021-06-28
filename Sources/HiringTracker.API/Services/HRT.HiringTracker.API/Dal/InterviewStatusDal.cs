

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IInterviewStatusDal))]
    public class InterviewStatusDal : DalBaseImpl<InterviewStatus, Interfaces.IInterviewStatusDal>, IInterviewStatusDal
    {

        public InterviewStatusDal(Interfaces.IInterviewStatusDal dalImpl) : base(dalImpl)
        {
        }

        public InterviewStatus Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

            }
}
