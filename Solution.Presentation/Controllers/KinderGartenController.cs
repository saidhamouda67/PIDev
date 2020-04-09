

using Microsoft.AspNet.Identity;
using Solution.Domain.Entities;
using Solution.Presentation.Models;
using Solution.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Solution.Presentation.Controllers
{
    public class KinderGartenController : Controller
    {
       
        IKindergartenService KindergartenService;
        IUserService userService;
        public KinderGartenController()
        {
            KindergartenService = new KindergartenService();
            userService = new UserService();
        }

        // GET: KinderGarten
        public ActionResult Index(string searchString)
        {
            var userId = User.Identity.GetUserId();
            String Phone2 = userService.GetById(userId).Phone2;
            String mail = userService.GetById(userId).Email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            var kindergartens = new List<KindergartenModel>();
            foreach (Kindergarten k in KindergartenService.SearchKindergartenByName(searchString))
            {
                KindergartenModel ks = new KindergartenModel()
                {
                    Address=k.Address,
                    Cost=k.Cost,
                    DateCreation=k.DateCreation,
                    Description=k.Description,
                    KindergartenId=k.KindergartenId,
                    Logo=k.Logo,
                    Name=k.Name,
                    NbrEmp=k.NbrEmp,
                    Phone=k.Phone
                };
                kindergartens.Add(ks);
               
            }
           

            return View(kindergartens);
        }

        // GET: KinderGarten/Details/5
        public ActionResult Details(int? id)
        {
            var userId = User.Identity.GetUserId();
            String Phone2 = userService.GetById(userId).Phone2;
            String mail = userService.GetById(userId).Email;
            ViewBag.home = mail;
            ViewBag.phone = Phone2;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kindergarten k;
            k = KindergartenService.GetById((int)id);
            if (k == null)
            {
                return HttpNotFound();
            }
            KindergartenModel ks = new KindergartenModel()
            {
                Address = k.Address,
                Cost = k.Cost,
                DateCreation = k.DateCreation,
                Description = k.Description,
                KindergartenId = k.KindergartenId,
                Logo = k.Logo,
                Name = k.Name,
                NbrEmp = k.NbrEmp,
                Phone = k.Phone,
                UserId = k.UserId,
                nameDir = userService.GetById(k.UserId).Email

            };
            return View(ks);
        }
        

        // GET: KinderGarten/Create

        public ActionResult Create()
        {
           

            return View();
        }


        // POST: KinderGarten/Create
        [HttpPost]
        public ActionResult Create(KindergartenModel km, HttpPostedFileBase Image)
        {
            Kindergarten kg = new Kindergarten();

            kg.Name = km.Name;
            kg.Logo = Image.FileName;
            kg.DateCreation = DateTime.UtcNow;
            kg.Address = km.Address;
            kg.Cost = km.Cost;
            kg.Description = km.Description;
            kg.NbrEmp = km.NbrEmp;
            kg.Phone = km.Phone;
            kg.UserId = User.Identity.GetUserId<string>();

            KindergartenService.Add(kg);
            KindergartenService.Commit();

            var path2 = Path.Combine(Server.MapPath("~/Content/Uploads"), Image.FileName);
            Image.SaveAs(path2);
            return RedirectToAction("Index");
        }

        // GET: KinderGarten/Edit/5
        public ActionResult Edit(int id)
        {
            Kindergarten t = KindergartenService.GetById(id);
            KindergartenModel tm = new KindergartenModel();

            tm.Name = t.Name;
            //ImageUrl = Image.FileName,
            tm.DateCreation = t.DateCreation;
            tm.Address = t.Address;
            tm.Cost = t.Cost;
            tm.Description = t.Description;
            tm.Phone = t.Phone;
            tm.NbrEmp = t.NbrEmp;
            return View(tm);
        }

        // POST: KinderGarten/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: KinderGarten/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: KinderGarten/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
