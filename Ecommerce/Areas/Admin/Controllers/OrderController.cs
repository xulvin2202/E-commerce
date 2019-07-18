using ClosedXML.Excel;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        EcommerceDbContext db = new EcommerceDbContext();
        // GET: Admin/Order
        public ActionResult Index(int page=1,int pageSize=10)
        {
            var dao = new OrderDao();
            var model = dao.ListOrder(page,pageSize);
            return View(model);
        }
        [HttpPost]
        public FileResult Export()
        {
            EcommerceDbContext entities = new EcommerceDbContext();
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("STT"),
                                            new DataColumn("Người nhận hàng"),
                                            new DataColumn("Điện thoại"),
                                            new DataColumn("Địa chỉ"),
                                            new DataColumn("Email") });

            var customers = from customer in entities.Orders.Take(10)
                            select customer;

            foreach (var customer in customers)
            {
                dt.Rows.Add(customer.ID, customer.ShipName, customer.ShipMobile, customer.ShipAddress, customer.ShipEmail);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Doanh thu.xlsx");
                }
            }
        }
    }
}