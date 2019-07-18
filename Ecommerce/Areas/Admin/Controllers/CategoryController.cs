using Common;
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
    public class CategoryController : BaseController
    {
        EcommerceDbContext db = new EcommerceDbContext();
        // GET: Admin/Category
        public ActionResult Index(int page = 1, int pageSize=12)
        {
            var dao = new CategoryDao();
            var model = dao.ListAllCategories(page,pageSize);
            return View(model);
        }        
        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }        
        [HttpPost]
        public ActionResult Create([Bind(Include ="ID,Name,MetaTitle,CreateDate,Image,Icon,ParentID,Status")]Category category,HttpPostedFileBase image)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var dao = new CategoryDao();
                    var path = "";
                    var filename = "";
                    if (image != null)
                    {
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + image.FileName;
                        path = Path.Combine(Server.MapPath("~/Image"), filename);
                        image.SaveAs(path);
                        category.Image = filename;
                    }
                    //else
                    //{

                    //    content.Image = "~/Image/logo.png";
                    //}
                    category.Name = category.Name;
                    category.Icon = category.Icon;
                    category.MetaTitle = StringHelper.ToUnsignString(category.Name);
                    category.CreateDate = Convert.ToDateTime(DateTime.UtcNow.ToLocalTime());
                    category.Status = Convert.ToBoolean(true);
                    category.ParentID = category.ParentID;
                    var id = dao.Insert(category);
                    if (id > 0)
                    {
                        SetAlert("Thêm mới thành thành công", "success");
                        ViewBag.Success = "Thêm thành công";
                        category = new Category();
                        return RedirectToAction("Index", "Category");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm mới ko thành công");
                    }
                }
            }
            catch(DbEntityValidationException e)
            {
                throw e;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return View(category);
        }  
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new CategoryDao();
            var content = dao.GetByID(id);
            return View();
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,Name,MetaTitle,CreateDate,Image,Icon,ParentID,Status")]Category category,HttpPostedFileBase image)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var dao = new CategoryDao();
                    var filename = "";
                    var path = "";
                    if (image != null)
                    {
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + image.FileName;
                        path = Path.Combine(Server.MapPath("~/Image"), filename);
                        image.SaveAs(path);
                        category.Image = filename;
                    }
                    category.Name = category.Name;
                    category.Icon = category.Icon;
                    category.MetaTitle = StringHelper.ToUnsignString(category.Name);
                    category.ParentID = category.ParentID;
                    category.Status = Convert.ToBoolean(true);
                    var result = dao.Update(category);
                    if (result)
                    {
                        SetAlert("Sửa thành công", "success");
                        ViewBag.Success = "Cập nhật thành công";
                        category = new Category();
                        return RedirectToAction("Index", "Category");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhật ko thành công");
                    }
                }
            }
            catch(DbEntityValidationException e)
            {
                throw e;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return View(category);
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new CategoryDao().Delete(id);
            return RedirectToAction("Index");
        }
       
    }
}