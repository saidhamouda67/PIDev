using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PM_Dashboard.App_Start;
using Solution.Data;
using Solution.Domain.Entities;
using Solution.Presentation.Models;
using Solution.Presentation.Models.Back;
using Solution.Presentation.Models.Forum;
using Solution.Presentation.Models.Post;
using Solution.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Solution.Presentation.Controllers
{
    public class BackOfficeController : Controller
    {
        private readonly ApplicationUserManager _userManager;

        IUserService us = new UserService();
        IForumService fs = new ForumService();
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly MyContext context = new MyContext();
        public BackOfficeController()
        {
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            _userManager = new ApplicationUserManager(new UserStore<User>(context));
           
        }
        public BackOfficeController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated )
            {
                var userId = User.Identity.GetUserId();
                if (_userManager.IsInRole(userId, "Admin"))
                {
                    var user = us.GetById(userId);
                    var model = new BackOfficeModel
                    {
                        UserName = user.UserName,
                        image = user.image
                    };
                    return View(model);
                }
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public JsonResult getRoles()
        {
            var roles = roleManager.Roles.ToList();



            return Json(roles, JsonRequestBehavior.AllowGet);
        }
      
        [HttpGet]
        public JsonResult getUsers()
        {
            try
            {
                context.Configuration.ProxyCreationEnabled = false;

                var users = context.Users.ToList();
                return Json(users, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            finally
            {
                //restore ProxyCreation to its original state
                context.Configuration.ProxyCreationEnabled = false;
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateRole(BackOfficeModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole ir = new IdentityRole
                {
                    Name = model.Role.Name
                };
                IdentityResult result = await roleManager.CreateAsync(ir);



            }

            return RedirectToAction("Index", "BackOffice");

        }

        [HttpPost]
        public async Task<ActionResult> CreateRoleAjax(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                IdentityRole ir = new IdentityRole
                {
                    Name = role.Name
                };
                IdentityResult result = await roleManager.CreateAsync(ir);



            }
            string message = "Success";
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });

        }
        [HttpPost]
        public async Task<ActionResult> DeleteRoleAjax(IdentityRole role)
        {
            var theRole = await roleManager.FindByIdAsync(role.Id);
            var users = context.Users.ToList();
            if (theRole!=null)
            {

                foreach (var user in users)
                {
                    if (_userManager.IsInRole(user.Id, theRole.Name))
                    {
                        IdentityResult rs = await _userManager.RemoveFromRoleAsync(user.Id, theRole.Name);
                    }
                    else continue;
                }
                IdentityResult result = await roleManager.DeleteAsync(theRole);

            }
        
        string message = "Success";
        return Json(new { Message = message, JsonRequestBehavior.AllowGet });
    
        }


        [HttpPost]
        public async Task<ActionResult> AffectRoleToUser(RoleUserModel roleUser)
        {
            var user = await _userManager.FindByIdAsync(roleUser.UserId);
            var role = await roleManager.FindByIdAsync(roleUser.RoleId);
            if(!user.EmailConfirmed)
                return Json(new {Message= "cet utilisateur n'est pas verifié déja", JsonRequestBehavior.AllowGet });

            if (user!=null && role != null && !(await _userManager.IsInRoleAsync(roleUser.UserId,role.Name)))
            {
                IdentityResult result = _userManager.AddToRole(roleUser.UserId, role.Name);
            }

            string message = "the  Role affected successfully";
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });


        }

        [HttpPost]
        public ActionResult getFilteredUsers(FilterUserModel filterUser)
        {
            try {
                context.Configuration.ProxyCreationEnabled = false;

                IEnumerable<User> users = new Collection<User>();

                users = context.Users.ToList();

                if (filterUser.Email != "vide")
                    users = users.Where(s => s.Email.Contains(filterUser.Email));

                if (filterUser.UserName != "vide")
                    users = users.Where(s => s.UserName.Contains(filterUser.UserName));

                ICollection<User> lastUsers = new Collection<User>();
                if (filterUser.roleName != "vide")
                {
                    foreach (var user in users)
                    {
                        if (_userManager.IsInRole(user.Id, filterUser.roleName))
                        {
                            lastUsers.Add(user);
                        }
                    }
                    return Json(lastUsers, JsonRequestBehavior.AllowGet);

                }



                return Json(users, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            finally
            {
                //restore ProxyCreation to its original state
                context.Configuration.ProxyCreationEnabled = true;
            }

        }


        [HttpPost]
        public ActionResult AddForum(AddForumModel forum)

        {
            Forum f = new Forum
            {
                Title = forum.Name,
                Descrition = forum.Description,
                Created=DateTime.Now
            };
            fs.Add(f);
            fs.Commit();
            string message = "Success";
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }

        
    }
}