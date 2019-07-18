using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDao
    {
        EcommerceDbContext db = null;
        public OrderDao()
        {
            db = new EcommerceDbContext();
        }
        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }
        public IEnumerable<Order> ListOrder(int page,int pageSize)
        {
            IQueryable<Order> model = db.Orders;
            return model.OrderBy(x => x.ID).ToPagedList(page,pageSize);
        }
    }
}
