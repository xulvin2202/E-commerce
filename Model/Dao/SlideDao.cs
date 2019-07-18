using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class SlideDao
    {
        EcommerceDbContext db = null;
        public SlideDao()
        {
            db = new EcommerceDbContext();
        }

        public IEnumerable<Slide> ListSlide(int page, int pageSize)
        {
            IQueryable<Slide> model = db.Slides;
            return model.OrderBy(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public long Insert(Slide qandA)
        {
            db.Slides.Add(qandA);
            db.SaveChanges();
            return qandA.ID;
        }
        public bool Update(Slide entity)
        {
            try
            {
                var qandA = db.Slides.Find(entity.ID);
                qandA.Link = entity.Link;
                qandA.Image = qandA.Image;
                qandA.ModifiedBy = entity.ModifiedBy;
                qandA.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
        public Slide GetByID(long id)
        {
            return db.Slides.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var qa = db.Slides.Find(id);
                db.Slides.Remove(qa);
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
