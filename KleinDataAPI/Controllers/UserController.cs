using KleinAppDataManager.Library.DataAccess;
using KleinAppDataManager.Library.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace KleinDataAPI.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
      
       [HttpGet]
        public UserModel GetBy()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();
            return data.GetUserById(userId).First();
        }

       
        
    }
}
