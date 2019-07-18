using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class MenuDao
    {
        EcommerceDbContext db = null;
        public MenuDao()
        {
            db = new EcommerceDbContext();
        }
        public IEnumerable<Menu> ListMenu()
        {
            IQueryable<Menu> model = db.Menus;
            return model.ToList();
        }
        public long Insert(Menu menu)
        {
            db.Menus.Add(menu);
            db.SaveChanges();
            return menu.ID;
        }
        public bool Update(Menu entity)
        {
            try
            {
                var qandA = db.Menus.Find(entity.ID);
                qandA.Text = entity.Text;
                qandA.Target = entity.Target;
                qandA.Link = entity.Link;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }
        }
        public Menu GetByID(long id)
        {
            return db.Menus.Find(id);
        }       
    }
}
