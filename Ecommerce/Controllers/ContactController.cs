using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var model = new ContactDao().GetActiveContact();
            return View(model);
        }

        [HttpPost]
        public JsonResult Send(string name, string mobile, string address, string email, string content)
        {
            var review = new Feedback();
            review.Name = name;
            review.Email = email;
            review.CreateDate = DateTime.Now;
            review.Phone = mobile;
            review.Content = content;
            review.Address = address;

            var id = new ContactDao().InsertReview(review);
            if (id > 0)
            {
                return Json(new
                {
                    status = Convert.ToBoolean(true)
                });
                //send mail
            }

            else
                return Json(new
                {
                    status = Convert.ToBoolean(false)
                }
                );
            review.Status = Convert.ToBoolean(true);
        }

    }
}