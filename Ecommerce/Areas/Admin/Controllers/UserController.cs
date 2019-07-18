using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Model.Dao;
using Model.EF;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        //[HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 8 )
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        //[HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            return View();
        }
        //[HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        //[HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(User user, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                
                var dao = new UserDao();
                if (dao.CheckUserName(user.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                if (dao.CheckUserEmail(user.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var a = new User();
                    var encrytedMd5Hash = Common.Encryptor.MD5Hash(user.Password);
                    user.Password = encrytedMd5Hash;
                    var path = "";
                    var filename = "";
                    if (image != null)
                    {
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + image.FileName;
                        path = Path.Combine(Server.MapPath("~/Image"), filename);
                        image.SaveAs(path);
                        a.Image = filename;//Luu ý
                    }
                    else
                    {
                        a.Image = "logo.png";
                    }
                    a.UserName = user.UserName;
                    a.Name = user.Name;
                    a.Phone = user.Phone;
                    a.Address = user.Address;
                    a.Email = user.Email;
                    a.Phone = user.Phone;
                    //a.GroupID = user.GroupID;
                    a.Status = true;
                    var id = dao.Insert(user);
                    if (id > 0)
                    {
                        SetAlert("Thêm thành công", "success");
                        ViewBag.Success = "Thêm thành công";
                        user = new User();
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm user ko thành công");
                    }
                }
                
            }
            //SetViewBag(user.GroupID);
            return View(user);

        }
        [HttpPost]
        //[HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(User user, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if(!string.IsNullOrEmpty(user.Password))
                {
                    var encrytedMd5Hash = Common.Encryptor.MD5Hash(user.Password);
                    user.Password = encrytedMd5Hash;
                }
                var a = new User();
                var path = "";
                var filename = "";
                if (image != null)
                {
                    filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + image.FileName;
                    path = Path.Combine(Server.MapPath("~/Image/"), filename);
                    image.SaveAs(path);
                    a.Image = filename; //Luu ý
                }
                else
                {
                    a.Image = "logo.png";
                }
                a.Name = user.Name;
                a.Phone = user.Phone;
                a.Address = user.Address;
                a.Email = user.Email;
                a.Phone = user.Phone;
                //a.GroupID = user.GroupID;
                a.Status = true;
                var result = dao.Update(user);
                if (result)
                {
                    SetAlert("Sửa thành công", "success");
                    ViewBag.Success = "Cập nhật thành công";
                    user = new User();
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật ko thành công");
                }
            }
            //SetViewBag(user.GroupID);
            return View(user);

        }
        [HttpDelete]
        //[HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        //[HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        //public void SetViewBag(string seletedID = null)
        //{
        //    var dao = new Model.Dao.UserDao();
        //    ViewBag.GroupID = new SelectList(dao.ListUserGroup(), "ID", "Name", seletedID);

        //}
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }

    }
}