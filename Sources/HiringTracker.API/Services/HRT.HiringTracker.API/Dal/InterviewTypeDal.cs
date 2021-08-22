

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IInterviewTypeDal))]
    public class InterviewTypeDal : DalBaseImpl<InterviewType, Interfaces.IInterviewTypeDal>, IInterviewTypeDal
    {

        public InterviewTypeDal(Interfaces.IInterviewTypeDal dalImpl) : base(dalImpl)
        {
        }

        public InterviewType Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

            }
}
