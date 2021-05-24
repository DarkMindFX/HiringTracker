using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.Interfaces.Entities
{
    public class User
    {        
        public long? UserID
        {
            get; set;
        }
       
        public string Login
        {
            get; set;
        }

      
        public string FirstName
        {
            get; set;
        }

       
        public string MiddleName
        {
            get; set;
        }
        
        public string LastName
        {
            get; set;
        }
     
        public string Email
        {
            get; set;
        }
       
        public string Description
        {
            get; set;
        }
      
        public string PasswordHash
        {
            get; set;
        }

        public string Salt
        {
            get; set;
        }
    }
}
