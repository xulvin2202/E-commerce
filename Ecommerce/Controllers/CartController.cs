using Ecommerce.Models;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Common;

namespace Ecommerce.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        EcommerceDbContext db = new EcommerceDbContext();
     
        //public bool isLogined()
        //{
        //    if (Session[CommonConstants.USER_SESSION] == null /*|| Session[CommonConstants.USER_PASSWORD] == null*/)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
        //// GET: Cart
        //public bool isLoginSucess(string id, string password)
        //{
        //    //Check user's id and password
        //    foreach (User a in db.Users)
        //    {
        //        if (a.UserName.Replace(" ", "") == id)
        //        {
        //            if (a.Password.Replace(" ", "") == password)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}
        public ActionResult Index()
        {

            
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            ViewBag.Total = Total();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public ActionResult AddItem(long productId, int quantity)
        {
            var product = new ProductDao().ViewDetail(productId);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ID == productId))
                {

                    foreach (var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Gán vào session
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }
        [HttpGet]
        public ActionResult Payment()
        {
            if (Session[CommonConstants.USER_SESSION] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var cart = Session[CartSession];
                var list = new List<CartItem>();
                if (cart != null)
                {
                    list = (List<CartItem>)cart;
                }

                return View(list);
            }
        }

        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address, string email)
        {

            var order = new Order();
            order.CreateDate = DateTime.Now;
            order.ShipAddress = address;
            order.ShipMobile = mobile;
            order.ShipName = shipName;
            order.ShipEmail = email;

            try
            {
                var id = new OrderDao().Insert(order);
                var cart = (List<CartItem>)Session[CartSession];
                var detailDao = new Model.Dao.OrderDetailDao();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Insert(orderDetail);

                    total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/template/neworder.html"));

                content = content.Replace("{{CustomerName}}", shipName);
                content = content.Replace("{{Phone}}", mobile);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(email, "Đơn hàng mới", content);
                new MailHelper().SendMail(toEmail, "Đơn hàng mới", content);
            }
            catch (Exception ex)
            {
                //ghi log
                return Redirect("/loi-thanh-toan");
            }
            Session[CartSession] = null;
            return Redirect("/hoan-thanh");
        }

        public ActionResult Success()
        {
            return View();
        }
        public ActionResult ErrorPayment()
        {
            return View();
        }
        private int TotalQuantity()
        {
            int iTotalQuantity = 0;
            List<CartItem> lstCart = Session[CartSession] as List<CartItem>;
            if (lstCart != null)
            {
                iTotalQuantity = lstCart.Sum(x => x.Quantity);
            }
            return iTotalQuantity;
        }
        private decimal? Total()
        {
            decimal? iTotal = 0;
            List<CartItem> lstCart = Session[CartSession] as List<CartItem>;
            //Session[CartSession] = list;
            if (lstCart != null)
            {
                iTotal = lstCart.Sum(x => x.TotalPrice);
            }
            return iTotal;
        }
    }
}