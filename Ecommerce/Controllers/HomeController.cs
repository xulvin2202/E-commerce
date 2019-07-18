using Common;
using Ecommerce.Models;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var model = new EcommerceDao().ListSlide();
            var product = new ProductDao();
            ViewBag.NewProduct = product.ListNewProduct(8);
            ViewBag.FeatureProduct = product.ListFeatureProduct(8);
            ViewBag.SaleProduct = product.ListSaleProduct(4);
            ViewBag.Brand = product.ListBrand(12);
            //ViewBag.Content = product.ListContent(6);
            return View(model);
        }
        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new EcommerceDao().ListByGroupId(1);
            return PartialView(model);
        }
        
        [ChildActionOnly]
        public ActionResult TopMenuRight()
        {
            var model = new EcommerceDao().ListByGroupId(2);
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult TopMenuLeft()
        {
            var model = new EcommerceDao().ListByGroupId(3);
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult FooterTop()
        {
            var model = new EcommerceDao().ListFooterByGroupId(1); 
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult FooterBottom()
        {
            var model = new EcommerceDao().ListFooterByGroupId(2);
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return PartialView(list);
        }




    }
}