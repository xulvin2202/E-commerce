using Model.Dao;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content
        public ActionResult Index(int page =1, int pageSize = 5)
        {
            var dao = new ContentDao();
            var model = dao.ListAllContentUI(page,pageSize);
            var m = new ContentDao();
            ViewBag.Cate = new ContentCategoryDao().ListAllContentCategory();

            //--
            //ViewBag.Cont = new ContentCategoryDao().Content();
            //--
            ViewBag.Content = m.ListContent(5);
            return View(model);
            
        }
        public ActionResult ListAllContent()
        {
            return View();
        }
        public ActionResult DetailContent(long id)
        {
            var content = new ContentDao().ViewDetail(id);
            ViewBag.ContentCategory = new ContentCategoryDao().ViewDetail(content.Content_Category_ID.Value);
            var model = new ContentDao();
            ViewBag.Content = model.ListContent(5);
            return View(content);
        }
        public ActionResult ContentCategory(long cateid, int page = 1/*, int pageSize = 2*/)
        {
            var contentcategory = new ContentCategoryDao().ViewDetail(cateid);
            ViewBag.ContentCategory = contentcategory;
            ViewBag.Cate = new ContentCategoryDao().ListAllContentCategory();
            var model = new ContentDao().ListContentByContentCategoryId(cateid);
            //--
            var mContent = new ContentCategoryDao().ViewDetail(cateid);
            ViewBag.Mcontent = mContent;


            var abc = new ContentDao();
            ViewBag.Content = abc.ListContent(5);
            var cont = new ContentCategoryDao().GetByID(cateid);
            ViewBag.Cont = cont;
            //--
            return View(model.ToPagedList(page, 8));
        }

    }
}