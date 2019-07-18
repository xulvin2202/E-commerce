using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Model.Dao
{
    public class ContactDao
    {
        EcommerceDbContext db = null;
        public ContactDao()
        {
            db = new EcommerceDbContext();
        }

        public Contact GetActiveContact()
        {
            return db.Contacts.Single(x => x.Status == true);
        }

        public int InsertReview(Feedback rv)
        {
            db.Feedbacks.Add(rv);
            db.SaveChanges();
            return rv.ID;
        }
        public IEnumerable<Feedback> ListAllFeedback( int page, int pageSize)
        {
            IQueryable<Feedback> model = db.Feedbacks;
            
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page,pageSize);
        }
        public bool Delete(int id)
        {
            try
            {
                var contact = db.Contacts.Find(id);
                db.Contacts.Remove(contact);
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
