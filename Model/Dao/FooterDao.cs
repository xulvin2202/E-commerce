using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class FooterDao
    {
        EcommerceDbContext db = null;
        public FooterDao()
        {
            db = new EcommerceDbContext();
        }
        public IEnumerable<Footer> ListFooterSub(int page, int pageSize)
        {
            IQueryable<Footer> model = db.Footers;
            return model.OrderBy(x => x.ParentID != null).ToPagedList(page, pageSize);
        }
        public IEnumerable<Footer> ListFooter()
        {
            IQueryable<Footer> model = db.Footers;
            return model.OrderBy(x => x.ID).ToList();
        }
        public long Insert(Footer footer)
        {
            db.Footers.Add(footer);
            db.SaveChanges();
            return footer.ID;
        }
        public bool Update(Footer entity)
        {
            try
            {
                var footer = db.Footers.Find(entity.ID);
                footer.Name = entity.Name;
                footer.Link = entity.Link;
                footer.DisplayOrder = entity.DisplayOrder;
                footer.ParentID = entity.ParentID;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
        public Footer GetByID(long id)
        {
            return db.Footers.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var qa = db.Footers.Find(id);
                db.Footers.Remove(qa);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
