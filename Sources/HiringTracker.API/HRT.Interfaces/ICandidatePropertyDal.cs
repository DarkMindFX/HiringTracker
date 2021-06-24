

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
        CandidateProperty Get(
                    System.Int64? ID
        );

        bool Delete(
                    System.Int64? ID
        );

                IList<CandidateProperty> GetByCandidateID(System.Int64 CandidateID);
            }
}

