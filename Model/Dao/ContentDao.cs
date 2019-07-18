using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
namespace Model.Dao
{
    public class ContentDao
    {
        EcommerceDbContext db = null;
        public ContentDao()
        {
            db = new EcommerceDbContext();
        }
        public List<ContentCategory> ListAllContentCategory()
        {
            return db.ContentCategories.Where(x => x.Status == true).ToList();
        }
        
        public long Insert(Content entity)
        {
            db.Contents.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public List<Content> ListContent(int id)
        {
            return db.Contents.OrderByDescending(x => x.CreateDate).Take(id).ToList();
        }
        public bool Update(Content entity)
        {
            try
            {
                var content = db.Contents.Find(entity.ID);
                content.Name = entity.Name;
                content.MetaTitle = entity.MetaTitle;
                content.Description = entity.Description;
                content.Image = entity.Image;
                content.Detail = entity.Detail;
                content.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
        public List<Content> ListRelatedContents(long contentID)
        {
            var content = db.Contents.Find(contentID);
            return db.Contents.Where(x => x.ID != contentID && x.Content_Category_ID == content.Content_Category_ID).ToList();
        }
        public IEnumerable<Content> ListAllContent(string searchStringContent, int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            if (!string.IsNullOrEmpty(searchStringContent))
            {
                model = model.Where(x => x.Name.Contains(searchStringContent) || x.MetaTitle.Contains(searchStringContent) || x.Detail.Contains(searchStringContent) || x.Description.Contains(searchStringContent));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page,pageSize);
        }
        //public IEnumerable<Content> ListAllContent(string searchString)
        //{
        //    IQueryable<Content> model = db.Contents;
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        model = model.Where(x => x.Name.Contains(searchString) || x.MetaTitle.Contains(searchString) || x.Description.Contains(searchString) || x.Detail.Contains(searchString) );
        //    }
        //    return model.OrderByDescending(x => x.CreateDate).ToList();
        //}
        public List<Content> ListContentByContentCategoryId(long categoryID)
        {

            return db.Contents.Where(x => x.Content_Category_ID == categoryID).ToList();
        }
        public bool Delete(int id)
        {
            try
            {
                var content = db.Contents.Find(id);
                db.Contents.Remove(content);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
       
        public IEnumerable<Content> ListAllContentUI()
        {
            IQueryable<Content> model = db.Contents;

            return model.OrderByDescending(x => x.CreateDate).ToList();
        }
        public IEnumerable<Content> ListAllContentUI(int page,int pageSize)
        {
            IQueryable<Content> model = db.Contents;

            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page,pageSize);
        }
        public Content ViewDetail(long id)
        {
            return db.Contents.Find(id);
        }
    }
}
