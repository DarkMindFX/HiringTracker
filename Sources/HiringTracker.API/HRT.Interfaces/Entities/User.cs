

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRT.Interfaces.Entities
{
    public class User 
    {
				public System.Int64? ID { get; set; }

				public System.String Login { get; set; }

				public System.String FirstName { get; set; }

				public System.String LastName { get; set; }

				public System.String Email { get; set; }

				public System.String Description { get; set; }

				public System.String PwdHash { get; set; }

				public System.String Salt { get; set; }

				
    }
}
