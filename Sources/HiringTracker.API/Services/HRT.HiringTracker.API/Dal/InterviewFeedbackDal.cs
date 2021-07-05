

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IInterviewFeedbackDal))]
    public class InterviewFeedbackDal : DalBaseImpl<InterviewFeedback, Interfaces.IInterviewFeedbackDal>, IInterviewFeedbackDal
    {

        public InterviewFeedbackDal(Interfaces.IInterviewFeedbackDal dalImpl) : base(dalImpl)
        {
        }

        public InterviewFeedback Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

        public IList<InterviewFeedback> GetByInterviewID(System.Int64 InterviewID)
        {
            return _dalImpl.GetByInterviewID(InterviewID);
        }
        public IList<InterviewFeedback> GetByInterviewerID(System.Int64 InterviewerID)
        {
            return _dalImpl.GetByInterviewerID(InterviewerID);
        }
        public IList<InterviewFeedback> GetByCreatedByID(System.Int64 CreatedByID)
        {
            return _dalImpl.GetByCreatedByID(CreatedByID);
        }
        public IList<InterviewFeedback> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            return _dalImpl.GetByModifiedByID(ModifiedByID);
        }
            }
}
