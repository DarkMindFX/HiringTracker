

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IInterviewDal))]
    public class InterviewDal : DalBaseImpl<Interview, Interfaces.IInterviewDal>, IInterviewDal
    {

        public InterviewDal(Interfaces.IInterviewDal dalImpl) : base(dalImpl)
        {
        }

        public Interview Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

        public IList<Interview> GetByProposalID(System.Int64 ProposalID)
        {
            return _dalImpl.GetByProposalID(ProposalID);
        }
        public IList<Interview> GetByInterviewTypeID(System.Int64 InterviewTypeID)
        {
            return _dalImpl.GetByInterviewTypeID(InterviewTypeID);
        }
        public IList<Interview> GetByInterviewStatusID(System.Int64 InterviewStatusID)
        {
            return _dalImpl.GetByInterviewStatusID(InterviewStatusID);
        }
        public IList<Interview> GetByCreatedByID(System.Int64 CreatedByID)
        {
            return _dalImpl.GetByCreatedByID(CreatedByID);
        }
        public IList<Interview> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            return _dalImpl.GetByModifiedByID(ModifiedByID);
        }
            }
}
