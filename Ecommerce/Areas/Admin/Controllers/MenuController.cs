using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        EcommerceDbContext db = new EcommerceDbContext();
        // GET: Admin/Menu
        public ActionResult Index()
        {
            var dao = new MenuDao();
            var model = dao.ListMenu();
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include ="ID,Text,Link,Status,Target")]Menu menu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new MenuDao();
                    menu.Text = menu.Text;
                    menu.Link = menu.Link;
                    menu.Target = menu.Target;
                    menu.Status = Convert.ToBoolean(true);
                    var id = dao.Insert(menu);
                    if (id > 0)
                    {
                        SetAlert("Thêm mới thành thành công", "success");
                        ViewBag.Success = "Thêm thành công";
                        menu = new Menu();
                        return RedirectToAction("Index", "Menu");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm mới ko thành công");
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(menu);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new MenuDao();
            var menu = dao.GetByID(id);
            return View();
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,Text,Link,Target,Status")]Menu menu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new MenuDao();
                    menu.Text = menu.Text;
                    menu.Link = menu.Link;
                    menu.Target = menu.Target;
                    menu.Status = Convert.ToBoolean(true);
                    var result = dao.Update(menu);
                    if (result)
                    {
                        SetAlert("Sửa thành công", "success");
                        ViewBag.Success = "Cập nhật thành công";
                        menu = new Menu();
                        return RedirectToAction("Index", "Menu");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhật ko thành công");
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(menu);
        }
        public ActionResult Detail(int? id)
        {
            var dao = new Menu();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }
    }
}