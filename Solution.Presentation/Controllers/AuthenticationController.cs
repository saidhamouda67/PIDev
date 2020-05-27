using Microsoft.AspNet.Identity.EntityFramework;
using PM_Dashboard.App_Start;
using Solution.Data;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace Solution.Presentation.Controllers
{
    public class ApiCredentials
    {
        public string password { get; set; }
        public string username { get; set; }
        public string userId { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }



    
    }
    public class AuthenticationController : ApiController
    {
        private ApplicationUserManager _userManager;
        MyContext _context = new MyContext();

        public AuthenticationController()
        {
            _userManager = new ApplicationUserManager(new UserStore<User>(_context));
        }
        // GET: Authentication



        [System.Web.Http.HttpPost]
        [Route("api/user-login/{username}")]
        public async System.Threading.Tasks.Task<IHttpActionResult> Post(string username,ApiCredentials ac)

        {
            var  user = await _userManager.FindByNameAsync(username);

            
            if (user!=null)
            {
                
                bool result = await _userManager.CheckPasswordAsync(user, ac.password);
                if (result)
                {
                    ApiCredentials theUser = new ApiCredentials
                    {
                        userId = user.Id,
                        firstName = user.FirstName,
                        lastName = user.LastName,
                        username = user.UserName
                    };
                    return Json(theUser);
                }
                else
                {
                    return BadRequest("wrong password! try again");
                }
            }
            else
            {
                return BadRequest("user with this username does not exist");
            }

            
        }

    }
}