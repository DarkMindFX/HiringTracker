using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IPositionCandidateStepDal))]
    public class PositionCandidateStepDal : DalBaseImpl<PositionCandidateStep, Interfaces.IPositionCandidateStepDal>, IPositionCandidateStepDal
    {

        public PositionCandidateStepDal(Interfaces.IPositionCandidateStepDal dalImpl) : base(dalImpl)
        {
        }

        public new IDictionary<long, PositionCandidateStep> GetAllAsDictionary()
        {
            var statuses = _dalImpl.GetAll();

            IDictionary<long, PositionCandidateStep> result = statuses.ToDictionary(s => s.StepID);

            return result;
        }
    }
}
