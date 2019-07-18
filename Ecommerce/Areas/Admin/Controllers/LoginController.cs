using Common;
using Ecommerce.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Ecommerce.Common;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password)/*, true*/);
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    ////userSession.GroupID = user.GroupID;
                    ////var listCredentials = dao.GetListCredential(model.UserName);

                    //Session.Add(CommonConstants.SESSION_CREDENTIALS, listCredentials);
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khoá.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
                //else if (result == -3)
                //{
                //    ModelState.AddModelError("", "Tài khoản của bạn không có quyền đăng nhập.");
                //}
                else
                {
                    ModelState.AddModelError("", "đăng nhập không đúng.");
                }
            }
            return View("Index");
        }
        //public ActionResult Login(LoginModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var dao = new UserDao();
        //        var result = dao.Login(model.UserName.ToUpper(), Encryptor.MD5Hash(model.Password).ToUpper());
        //        switch (result)
        //        {
        //            case 1:
        //                var user = dao.GetById(model.UserName);
        //                var userSession = new UserLogin();
        //                userSession.UserName = user.UserName;
        //                userSession.UserID = user.ID;
        //                Session.Add(CommonConstants.USER_SESSION, userSession);
        //                return RedirectToAction("Index", "User");
        //                break;
        //            case 0:
        //                ModelState.AddModelError("", "Tài khoản không tồn tại");
        //                break;
        //            case -1:
        //                ModelState.AddModelError("", "Tài khoản đang bị khóa");
        //                break;
        //            case -2:
        //                ModelState.AddModelError("", "Mật khẩu sai");
        //                break;
        //            case -3:
        //                ModelState.AddModelError("", "Không được cấp quyền");
        //                break;
        //            default:
        //                ModelState.AddModelError("", "Tài khoản không đúng");
        //                break;
        //        }
        //    }
        //    return View("Index");
        //}
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }
    }
}