using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class EcommerceDao
    {
        EcommerceDbContext db = null;
        public EcommerceDao()
        {
            db = new EcommerceDbContext();
        }
        public List<Slide> ListSlide()
        {
            return db.Slides.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
        //public List<ContentCategory> ListAllContent()
        //{
        //    return db.ContentCategories.Where(x => x.Status == true).ToList();
        //}
        

        public Content GetByID(long id)
        {
            return db.Contents.Find(id);
        }
        public List<Category> ListAllCategory()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }
        

        public List<Menu> ListByGroupId(int groupId)
        {
            return db.Menus.Where(x => x.MenuType_ID == groupId && x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
        
        public List<Footer> ListFooterByGroupId(int groupId)
        {
            return db.Footers.Where(x => x.FooterType_ID == groupId && x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
        
       
        
        
        
        
        
        

    }
}
