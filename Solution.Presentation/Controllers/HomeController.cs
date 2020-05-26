using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PM_Dashboard.App_Start;
using Solution.Data;
using Solution.Domain.Entities;
using Solution.Presentation.Models;
using Solution.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solution.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly MyContext context = new MyContext();

        public HomeController()
        {
            _userManager = new ApplicationUserManager(new UserStore<User>(context));

        }
     
        IUserService us = new UserService();
        public ActionResult Index()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (!val1)
                return View();

            var userId = User.Identity.GetUserId();
            if (_userManager.IsInRole(userId, "Admin") || _userManager.IsInRole(userId, "Manager"))
            {
                ViewBag.Admin = true;
            }
            var user = us.GetById(userId);
            string Phone2 = user.Phone2;
            string mail = user.Email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            ViewBag.authenticated = val1;
            ViewBag.ImageUrlProfile = user.image;
            return View();
        }

        [HttpPost]
        public  ActionResult Update(EditViewModel model, HttpPostedFileBase Image)
        {
            IUserService us = new UserService();
            var userid = User.Identity.GetUserId();
            var theUser = us.GetById(userid);
            theUser.FirstName = model.FirstName;
            theUser.Email = model.Email;
            theUser.UserName = model.UserName;
            theUser.LastName = model.LastName;
            theUser.Phone2 = model.Phone2;
            theUser.DateOfBirth = model.DateOfBirth;
            if (Image != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(Image.FileName);
                string extention = Path.GetExtension(Image.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssff") + extention;
                model.Image = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                Image.SaveAs(fileName);
                theUser.image = model.Image;
            }
           
            us.Update(theUser);
            us.Commit();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult Update()
        {
            IUserService us = new UserService();

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");

            }


            var userId = User.Identity.GetUserId();

            var user = us.GetById(userId);
            var model = new EditViewModel
            {
                FirstName = user.FirstName,
                Email = user.Email,
                UserName = user.UserName,
                LastName = user.LastName,
                Phone2 = user.Phone2,
                DateOfBirth = user.DateOfBirth,
            };
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (!val1)
                return View();
            if (_userManager.IsInRole(userId, "Admin") || _userManager.IsInRole(userId, "Manager"))
            {
                ViewBag.Admin = true;
            }
            string Phone2 = us.GetById(userId).Phone2;
            string mail = us.GetById(userId).Email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            ViewBag.authenticated = val1;
           
            ViewBag.ImageUrlProfile = user.image;
            return View(model);


        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}