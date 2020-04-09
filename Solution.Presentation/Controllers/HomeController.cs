using Microsoft.AspNet.Identity;
using Solution.Presentation.Models;
using Solution.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solution.Presentation.Controllers
{
    public class HomeController : Controller
    {
        IUserService us = new UserService();
        public ActionResult Index()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (!val1)
                return View();
            var userId = User.Identity.GetUserId();
            string Phone2 = us.GetById(userId).Phone2;
            string mail = us.GetById(userId).Email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            ViewBag.authenticated = val1;
            return View();
        }

        [HttpPost]
        public  ActionResult Update(EditViewModel model)
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


            var model = new EditViewModel
            {
                FirstName = us.GetById(userId).FirstName,
                Email = us.GetById(userId).Email,
                UserName = us.GetById(userId).UserName,
                LastName = us.GetById(userId).LastName,
                Phone2 = us.GetById(userId).Phone2,
                Image = us.GetById(userId).image,
                DateOfBirth = us.GetById(userId).DateOfBirth
            };
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (!val1)
                return View();
            string Phone2 = us.GetById(userId).Phone2;
            string mail = us.GetById(userId).Email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            ViewBag.authenticated = val1;
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