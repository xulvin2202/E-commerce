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
    public class ProductDao
    {
        EcommerceDbContext db = null;
        public ProductDao()
        {
            db = new EcommerceDbContext();
        }
        public bool Delete(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<Product> ListNewProduct(int top)
        {
            return db.Products.Where(x => x.PromotionPrice == null).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }
        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }
        public Product GetByID(long id)
        {
            return db.Products.Find(id);
        }
        public bool Update(Product entity)
        {
            try
            {
                var product = db.Products.Find(entity.ID);
                product.Name = entity.Name;
                product.MetaTitle = entity.MetaTitle;
                product.Description = entity.Description;
                product.Image = entity.Image;
                product.Detail = entity.Detail;
                product.Price = entity.Price;
                product.PromotionPrice = entity.PromotionPrice;
                product.Status = product.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
        public IEnumerable<Product> ListAllProductAdmin(int page,int pageSize)
        {
            IQueryable<Product> model = db.Products;

            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<Product> ListAllProduct()
        {
            IQueryable<Product> model = db.Products;

            return model.OrderByDescending(x => x.CreateDate).ToList();
        }
        public List<Category> ListAllCategory()
        {
            return db.Categories.Where(x => x.Status == true && x.ParentID != null).ToList();
        }


        public List<Brand> ListAllBrand()
        {
            return db.Brands.Where(x => x.Status == true).ToList();
        }
        public IEnumerable<Product> ListAllPaging(string searchString/*, int page, int pageSize*/)
        {
            IQueryable<Product> model = db.Products.OrderByDescending(x => x.CreateDate);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreateDate);
        }

        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }
        public List<ProductViewModel> Listbestseller(long categoryID)
        {
            var model = (from a in db.Products
                         join b in db.Brands
                         on a.Brand_ID equals b.ID
                         where a.Brand_ID == categoryID
                         select new
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreateDate = a.CreateDate,
                             ID = a.ID,
                             Image = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Price = a.Price
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreateDate = x.CreateDate,
                             ID = x.ID,
                             Image = x.Image,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price
                         });
            return model.ToList();
        }
        public bool ChangeStatus(long id)
        {
            var product = db.Products.Find(id);
            product.Status = !product.Status;
            db.SaveChanges();
            return product.Status;
        }
        public List<Product> ListSaleProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.PromotionPrice).Take(top).ToList();
        }
        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
        public List<Category> ListSubCategory(long CategoryID)
        {
            return db.Categories.Where(x => x.ParentID != null && x.ParentID == CategoryID).OrderBy(x => x.DisplayOrder).ToList();
        }
        public long Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public List<Product> ListRelatedProducts(long productID)
        {
            var product = db.Products.Find(productID);
            return db.Products.Where(x => x.ID != productID && x.Category_ID == product.Category_ID).ToList();
        }
        //public List<Content> ListContent(int id)
        //{
        //    return db.Contents.OrderByDescending(x => x.CreateDate).Take(id).ToList();
        //}

        public List<Brand> ListBrandByCategoryId(long brandID)
        {
            return db.Brands.Where(x => x.Category_ID == brandID).ToList();
        }
        public List<Brand> ListBrand(int id)
        {
            return db.Brands.OrderByDescending(x => x.CreateDate).Take(id).ToList();
        }
        public List<Product> ListByCategoryId(long categoryID)
        {

            return db.Products.Where(x => x.Category_ID == categoryID).ToList();
        }
        public List<Product> ListByBrandId(long brandID)
        {
            return db.Products.Where(x => x.Brand_ID == brandID).ToList();
        }
        public List<ProductViewModel> ListByCategoryId(long categoryID, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.Category_ID == categoryID).Count();
            var model = (from a in db.Products
                         join b in db.Categories
                         on a.Category_ID equals b.ID
                         where a.Category_ID == categoryID
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
            model.OrderByDescending(x => x.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
        public List<ProductViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.Name == keyword).Count();
            var model = (from a in db.Products
                         join b in db.Categories
                         on a.Category_ID equals b.ID
                         where a.Name.Contains(keyword)
                         select new
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreatedDate = a.CreateDate,
                             ID = a.ID,
                             Image = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Price = a.Price
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreateDate = x.CreatedDate,
                             ID = x.ID,
                             Image = x.Image,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price
                         });
            model.OrderByDescending(x => x.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
    }
}
