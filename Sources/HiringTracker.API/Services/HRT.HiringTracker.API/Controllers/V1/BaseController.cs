using HRT.Interfaces.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRT.HiringTracker.API.Controllers.V1
{
    public class BaseController : ControllerBase
    {
        public User CurrentUser
        {
            get
            {
                return HttpContext.Items["User"] as User;
            }
        }
    }
}
