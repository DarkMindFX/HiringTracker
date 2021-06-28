

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(ICandidatePropertyDal))]
    public class CandidatePropertyDal : DalBaseImpl<CandidateProperty, Interfaces.ICandidatePropertyDal>, ICandidatePropertyDal
    {

        public CandidatePropertyDal(Interfaces.ICandidatePropertyDal dalImpl) : base(dalImpl)
        {
        }

        public CandidateProperty Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

        public IList<CandidateProperty> GetByCandidateID(System.Int64 CandidateID)
        {
            return _dalImpl.GetByCandidateID(CandidateID);
        }
            }
}
