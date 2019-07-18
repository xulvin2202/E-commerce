using Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class AboutController : BaseController
    {
        // GET: Admin/About
        EcommerceDbContext db = new EcommerceDbContext();
        // GET: Admin/Product

        public ActionResult IndexBrand(int page = 1, int pageSize = 12)
        {
            var dao = new BrandDao();
            var model = dao.ListBrand(page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult CreateBrand()
        {
            SetViewBagBrand();
            return View();
        }
        [HttpDelete]
        public ActionResult DeleteBrand(int id)
        {
            new BrandDao().Delete(id);
            return RedirectToAction("IndexBrand");
        }
        public ActionResult EditBrand(long id)
        {
            var dao = new BrandDao();
            var brand = dao.GetByID(id);
            SetViewBagBrand(brand.Category_ID);
            return View();
        }
        [HttpPost]
        public ActionResult EditBrand([Bind(Include = "ID,Name,MetaTitle,Image,CreateDate,CreateBy,ModifiedDate,ModifiedBy,Category_ID,Status")]Brand model, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var dao = new BrandDao();

                var a = new Brand();
                var path = "";
                var filename = "";
                if (image != null)
                {
                    filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + image.FileName;
                    path = Path.Combine(Server.MapPath("~/Image"), filename);
                    image.SaveAs(path);
                    model.Image = filename;
                }
                //else
                //{

                //    content.Image = "~/Image/logo.png";
                //}
                model.Name = model.Name;
                model.CreateDate = Convert.ToDateTime(DateTime.UtcNow.ToLocalTime());
                model.MetaTitle = StringHelper.ToUnsignString(model.Name);
                model.Category_ID = model.Category_ID;
                model.Status = Convert.ToBoolean(true);
                var result = dao.Update(model);
                if (result)
                {
                    SetAlert("Sửa thành công", "success");
                    ViewBag.Success = "Cập nhật thành công";
                    model = new Brand();
                    return RedirectToAction("IndexBrand", "About");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật ko thành công");
                }

            }
            SetViewBagBrand(model.Category_ID);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateBrand([Bind(Include = "ID,Name,MetaTitle,Image,CreateDate,CreateBy,Category_ID,Status")]Brand brand, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new BrandDao();
                    var filename = "";
                    var path = "";
                    if (image != null)
                    {
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + image.FileName;
                        path = Path.Combine(Server.MapPath("~/Image"), filename);
                        image.SaveAs(path);
                        brand.Image = filename;
                    }
                    //else
                    //{

                    //    content.Image = "~/Image/logo.png";
                    //}
                    brand.Name = brand.Name;
                    brand.CreateDate = Convert.ToDateTime(DateTime.UtcNow.ToLocalTime());
                    brand.MetaTitle = StringHelper.ToUnsignString(brand.Name);
                    brand.Category_ID = brand.Category_ID;
                    brand.Status = Convert.ToBoolean(true);
                    var id = dao.Insert(brand);
                    if (id > 0)
                    {
                        SetAlert("Thêm mới thành thành công", "success");
                        ViewBag.Success = "Thêm thành công";
                        brand = new Brand();
                        return RedirectToAction("IndexBrand", "About");
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
            SetViewBagBrand();
            return View(brand);
        }
        public void SetViewBagBrand(long? seletedID = null)
        {
            var dao = new Model.Dao.CategoryDao();
            ViewBag.Category_ID = new SelectList(dao.ListCategory(), "ID", "Name", seletedID);

        }
    }
}