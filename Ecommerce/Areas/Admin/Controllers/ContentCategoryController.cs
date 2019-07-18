using Common;
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
    public class ContentCategoryController : BaseController
    {
        private EcommerceDbContext db = new EcommerceDbContext();

        public ActionResult Index()
        {
            var dao = new ContentCategoryDao();
            var model = dao.ListAllContentCategory();
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var contentCategory = new ContentCategoryDao().ViewDetail(id);
            return View(contentCategory);
        }
        [HttpPost]
        //[HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(ContentCategory contentCategory)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContentCategoryDao();
                var a = new ContentCategory();
                a.Name = contentCategory.Name;
                a.CreateDate = Convert.ToDateTime(DateTime.UtcNow.ToLocalTime());
                a.MetaTitle = StringHelper.ToUnsignString(a.Name);
                a.Status = Convert.ToBoolean(true);
                var id = dao.Insert(a);
                if (id > 0)
                {
                    SetAlert("Thêm thành công", "success");
                    ViewBag.Success = "Thêm thành công";
                    contentCategory = new ContentCategory();
                    return RedirectToAction("Index", "ContentCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm user ko thành công");
                }
            }
            return View(contentCategory);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,Name,MetaTitle,CreateDate,Status")]ContentCategory model)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContentCategoryDao();
                var a = new ContentCategory();
                model.Name = model.Name;
                model.MetaTitle = StringHelper.ToUnsignString(model.Name);
                model.Status = Convert.ToBoolean(true);
                var result = dao.Update(model);
                if (result)
                {
                    SetAlert("Sửa thành công", "success");
                    ViewBag.Success = "Cập nhật thành công";
                    model = new ContentCategory();
                    return RedirectToAction("Index", "ContentCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật ko thành công");
                }
            }
            return View();
        }
        [HttpDelete]
        //[HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(int id)
        {
            new ContentCategoryDao().Delete(id);
            return RedirectToAction("Index");
        }



    }
}