using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class AboutController : Controller
    {
        EcommerceDbContext db = new EcommerceDbContext();
        // GET: About
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult Index(int? id)
        //{
        //    var dao = new AboutDao();
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    About about = db.Abouts.Find(id);
        //    if (about == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(about);
        //}
    }
}