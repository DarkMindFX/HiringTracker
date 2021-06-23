

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRT.Interfaces.Entities;

namespace HRT.Interfaces
{
    public interface ICandidatePropertyDal : IDalBase<CandidateProperty>
    {
                IList<CandidateProperty> GetByCandidateID(System.Int64 CandidateID);
            }
}

