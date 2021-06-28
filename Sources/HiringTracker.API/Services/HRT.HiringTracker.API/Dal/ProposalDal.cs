

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IProposalDal))]
    public class ProposalDal : DalBaseImpl<Proposal, Interfaces.IProposalDal>, IProposalDal
    {

        public ProposalDal(Interfaces.IProposalDal dalImpl) : base(dalImpl)
        {
        }

        public Proposal Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

        public IList<Proposal> GetByPositionID(System.Int64 PositionID)
        {
            return _dalImpl.GetByPositionID(PositionID);
        }
        public IList<Proposal> GetByCandidateID(System.Int64 CandidateID)
        {
            return _dalImpl.GetByCandidateID(CandidateID);
        }
        public IList<Proposal> GetByCurrentStepID(System.Int64 CurrentStepID)
        {
            return _dalImpl.GetByCurrentStepID(CurrentStepID);
        }
        public IList<Proposal> GetByNextStepID(System.Int64? NextStepID)
        {
            return _dalImpl.GetByNextStepID(NextStepID);
        }
        public IList<Proposal> GetByStatusID(System.Int64 StatusID)
        {
            return _dalImpl.GetByStatusID(StatusID);
        }
        public IList<Proposal> GetByCreatedByID(System.Int64? CreatedByID)
        {
            return _dalImpl.GetByCreatedByID(CreatedByID);
        }
        public IList<Proposal> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            return _dalImpl.GetByModifiedByID(ModifiedByID);
        }
            }
}
