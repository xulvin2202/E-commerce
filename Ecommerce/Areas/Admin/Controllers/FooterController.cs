using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class FooterController : BaseController
    {
        EcommerceDbContext db = new EcommerceDbContext();
        public ActionResult Index()
        {
            var dao = new FooterDao();
            var model = dao.ListFooter();
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new FooterDao().Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(long id)
        {
            var dao = new FooterDao();
            var footer = dao.GetByID(id);
      
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,Name,Link,DisplayOrder,ParentID,Status")]Footer footer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new FooterDao();

                    footer.Name = footer.Name;
                    footer.Link = footer.Link;
                    footer.DisplayOrder = footer.DisplayOrder;
                    footer.ParentID = footer.ParentID;
                    footer.Status = Convert.ToBoolean(true);
                    var id = dao.Insert(footer);
                    if (id > 0)
                    {
                        SetAlert("Thêm mới thành thành công", "success");
                        ViewBag.Success = "Thêm thành công";
                        footer = new Footer();
                        return RedirectToAction("Index", "Footer");
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
            return View(footer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,Name,Link,DisplayOrder,Status")]Footer footer)
        {

            if (ModelState.IsValid)
            {
                var dao = new FooterDao();
                footer.Name = footer.Name;
                footer.Link = footer.Link;
                footer.DisplayOrder = footer.DisplayOrder;
                footer.Status = Convert.ToBoolean(true);
                var result = dao.Update(footer);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    ViewBag.Success = "Thêm thành công";
                    footer = new Footer();
                    return RedirectToAction("Index", "Footer");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật ko thành công");
                }
            }
            return View(footer);
        }
    }
}