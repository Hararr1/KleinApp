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
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
      
       [HttpGet]  
       [Route("GetInfo")]
        public UserModel GetBy()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();
            return data.GetUserById(userId).First();
        }
        [HttpGet]
        [Route("GetId")]
        public string GetId()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            return userId;
        }





    }
}
