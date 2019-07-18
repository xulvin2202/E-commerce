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
    public class ProductController : BaseController
    {
        EcommerceDbContext db = new EcommerceDbContext();
        // GET: Admin/Product
        public ActionResult Index(int page = 1, int pageSize = 12)
        {
            var dao = new ProductDao();
            var model = dao.ListAllProductAdmin(page,pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,Name,MetaTitle,Price,PromotionPrice,Description,Detail,Image,CreateDate,CreateBy,ModifiedDate,ModifiedBy,MetaKeywords,MetaDescription,Category_ID,Brand_ID,Status")]Product product, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new ProductDao();
                    var filename = "";
                    var path = "";
                    if (image != null)
                    {
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + image.FileName;
                        path = Path.Combine(Server.MapPath("~/Image"), filename);
                        image.SaveAs(path);
                        product.Image = filename;
                    }
                    //else
                    //{

                    //    content.Image = "~/Image/logo.png";
                    //}
                    product.Name = product.Name;
                    product.CreateDate = Convert.ToDateTime(DateTime.UtcNow.ToLocalTime());
                    product.MetaTitle = StringHelper.ToUnsignString(product.Name);
                    product.Description = product.Description;
                    product.Detail = product.Detail;
                    product.Price = product.Price;
                    product.PromotionPrice = product.PromotionPrice;
                    product.Category_ID = product.Category_ID;
                    product.Brand_ID = product.Brand_ID;
                    product.Status = Convert.ToBoolean(true);
                    var id = dao.Insert(product);
                    if (id > 0)
                    {
                        SetAlert("Thêm mới thành thành công", "success");
                        ViewBag.Success = "Thêm thành công";
                        product = new Product();
                        return RedirectToAction("Index", "Content");
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
            SetViewBag();
            return View(product);
        }
        public ActionResult Edit(long id)
        {
            var dao = new ProductDao();
            var product = dao.GetByID(id);
            SetViewBag(product.Category_ID);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,Name,MetaTitle,Description,Detail,Image,CreateDate,Brand_ID,Price,PromotionPrice,Category_ID,Status")]Product product, HttpPostedFileBase image)
        {

            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                var filename = "";
                var path = "";
                if (image != null)
                {
                    filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + image.FileName;
                    path = Path.Combine(Server.MapPath("~/Image"), filename);
                    image.SaveAs(path);
                    product.Image = filename;
                }
                //else
                //{

                //    content.Image = "~/Image/logo.png";
                //}
                product.Name = product.Name;
                product.MetaTitle = StringHelper.ToUnsignString(product.Name);
                product.Description = product.Description;
                product.Detail = product.Detail;
                product.Category_ID = product.Category_ID;
                product.Brand_ID = product.Brand_ID;
                product.Price = product.Price;
                product.PromotionPrice = product.PromotionPrice;
                product.Status = product.Status;
                var result = dao.Update(product);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    ViewBag.Success = "Thêm thành công";
                    product = new Product();
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật ko thành công");
                }
            }

            SetViewBag();
            return View(product);
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductDao().Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult Detail(int? id)
        {
            var dao = new ProductDao();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        public void SetViewBag(long? seletedID = null)
        {
            var dao = new Model.Dao.ProductDao();
            ViewBag.Category_ID = new SelectList(dao.ListAllCategory(), "ID", "Name", seletedID);
            ViewBag.Brand_ID = new SelectList(dao.ListAllBrand(), "ID", "Name", seletedID);
        }
    }
}