using Model.EF;
using Model.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class BrandDao
    {
        EcommerceDbContext db = null;
        public BrandDao()
        {
            db = new EcommerceDbContext();
        }
        public Brand GetByID(long id)
        {
            return db.Brands.Find(id);
        }
        public List<Brand> ListBrand()
        {
            return db.Brands.OrderByDescending(x => x.CreateDate).ToList();
        }
        public IEnumerable<Brand> ListAllBrand()
        {
            IQueryable<Brand> model = db.Brands;

            return model.OrderByDescending(x => x.CreateDate).ToList();
        }
        public IEnumerable<Brand> ListBrand(int page, int pageSize)
        {
            IQueryable<Brand> model = db.Brands;

            return model.OrderBy(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public bool Update(Brand entity)
        {
            try
            {
                var brand = db.Brands.Find(entity.ID);
                brand.Name = entity.Name;
                brand.MetaTitle = entity.MetaTitle;
                brand.Link = entity.Link;
                brand.Image = entity.Image;
                brand.CreateDate = DateTime.Now;
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
                var brand = db.Brands.Find(id);
                db.Brands.Remove(brand);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public List<ProductViewModel> ListByCategoryId(long brandID)
        {

            var model = (from a in db.Products
                         join b in db.Brands
                         on a.Brand_ID equals b.ID
                         where a.Brand_ID == brandID
                         select new
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreatedDate = a.CreateDate,
                             ID = a.ID,
                             Images = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Price = a.Price,
                             PromotionPrice = a.PromotionPrice
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreateDate = x.CreatedDate,
                             ID = x.ID,
                             Image = x.Images,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price,
                             PromotionPrice = x.Price
                         });
            model.OrderByDescending(x => x.CreateDate);
            return model.ToList();
        }
        //public List<Brand> ListAllBrand()
        //{
        //    return db.Brands.Where(x => x.Status == true).OrderByDescending(x => x.CreateDate).ToList();
        //}
        public long Insert(Brand brand)
        {
            db.Brands.Add(brand);
            db.SaveChanges();
            return brand.ID;
        }
        public List<Category> ListAllCategory()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }
        public Brand ViewDetail(long id)
        {
            return db.Brands.Find(id);
        }
    }
}