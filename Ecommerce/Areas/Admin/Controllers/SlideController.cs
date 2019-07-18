using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
        // GET: Admin/Slide
        EcommerceDbContext db = new EcommerceDbContext();
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            var dao = new SlideDao();
            var model = dao.ListSlide(page,pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,Image, Link, CreateDate, Status")]Slide slide, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new SlideDao();
                    var path = "";
                    var filename = "";
                    if (image != null)
                    {
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + image.FileName;
                        path = Path.Combine(Server.MapPath("~/Image"), filename);
                        image.SaveAs(path);
                        slide.Image = filename;
                    }
                    //else
                    //{

                    //    content.Image = "~/Image/logo.png";
                    //}
                    slide.Link = slide.Link;
                    slide.CreateDate = Convert.ToDateTime(DateTime.UtcNow.ToLocalTime());
                    slide.Status = Convert.ToBoolean(true);
                    var id = dao.Insert(slide);
                    if (id > 0)
                    {
                        SetAlert("Thêm mới thành thành công", "success");
                        ViewBag.Success = "Thêm thành công";
                        slide = new Slide();
                        return RedirectToAction("Index", "Slide");
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
            return View(slide);
        }
        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,Image, Link, CreateDate, Status")]Slide slide, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var path = "";
                    var filename = "";
                    var dao = new SlideDao();
                    if (image != null)
                    {
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + image.FileName;
                        path = Path.Combine(Server.MapPath("~/Image"), filename);
                        image.SaveAs(path);
                        slide.Image = filename;
                    }
                    //else
                    //{
                    //    content.Image = "~/Image/logo.png";
                    //}
                    slide.Link = slide.Link;
                    slide.Status = Convert.ToBoolean(true);
                    var result = dao.Update(slide);
                    if (result)
                    {
                        SetAlert("Sửa thành công", "success");
                        ViewBag.Success = "Cập nhật thành công";
                        slide = new Slide();
                        return RedirectToAction("Index", "Product");
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
            return View(slide);
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new SlideDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}