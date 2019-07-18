using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        EcommerceDbContext db = new EcommerceDbContext();
        // GET: Admin/Contact
        public ActionResult Index( int page = 1, int pageSize = 8)
        {
            var dao = new ContactDao();
            var model = dao.ListAllFeedback( page, pageSize);
            return View(model);
        }
        public ActionResult Detail(int? id)
        {
            var dao = new ProductDao();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback  = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }
        
    }
}