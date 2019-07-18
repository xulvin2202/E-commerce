using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class UserGroupController : Controller
    {
        EcommerceDbContext db = new EcommerceDbContext();
        // GET: Admin/UserGroup
        //public ActionResult Index()
        //{
        //    var dao = new UserDao();
        //    //var model = dao.ListUserGroup();
        //    return View(model);
        //}
    }
}