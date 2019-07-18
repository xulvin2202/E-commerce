using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Dao;

namespace Model.Dao
{
    public class ContentCategoryDao
    {
        EcommerceDbContext db = null;

        public ContentCategoryDao()
        {
            db = new EcommerceDbContext();
        }
        public long Insert(ContentCategory entity)
        {
            db.ContentCategories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        
        public ContentCategory GetByID(long id)
        {
            return db.ContentCategories.Find(id);
        }
        public IEnumerable<ContentCategory> ListAllContentCategory(string searchString)
        {
            IQueryable<ContentCategory> model = db.ContentCategories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.MetaTitle.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToList();
        }
        public IEnumerable<ContentCategory> ListAllContentCategory()
        {
            IQueryable<ContentCategory> model = db.ContentCategories;

            return model.OrderByDescending(x => x.CreateDate).ToList();
        }
       
        public bool Update(ContentCategory contentCategory)
        {
            try
            {
                var content = db.ContentCategories.Find(contentCategory.ID);
                content.Name = contentCategory.Name;
                content.MetaTitle = contentCategory.MetaTitle;
                content.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
       
        public bool Delete(int id)
        {
            try
            {
                var contentCategory = db.ContentCategories.Find(id);
                db.ContentCategories.Remove(contentCategory);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public ContentCategory ViewDetail(long id)
        {
            return db.ContentCategories.Find(id);
        }

    }
}
