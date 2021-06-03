using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using {DalNamespace}.Entities;

namespace {DalNamespace}
{
    public interface I{Entity}Dal : IDalBase<{Entity}>
    {
    }
}
