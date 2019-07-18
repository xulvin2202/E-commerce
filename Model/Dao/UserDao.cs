using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.Dao
{
    public class UserDao
    {
        EcommerceDbContext db = null;
        public UserDao()
        {
            db = new EcommerceDbContext();
        }
        //public int Login(string userName, string passWord, bool isLoginAdmin = false)
        //{
        //    var result = db.Users.SingleOrDefault(x => x.UserName == userName);
        //    if (result == null)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        if (isLoginAdmin == true)
        //        {
        //            if (result.GroupID == Common.Constants.ADMIN_GROUP || result.GroupID == Common.Constants.MOD_GROUP)
        //            {
        //                if (result.Status == false)
        //                {
        //                    return -1;
        //                }
        //                else
        //                {
        //                    if (result.Password == passWord)
        //                        return 1;
        //                    else
        //                        return -2;
        //                }
        //            }
        //            else
        //            {
        //                return -3;
        //            }
        //        }
        //        else
        //        {
        //            if (result.Status == false)
        //            {
        //                return -1;
        //            }
        //            else
        //            {
        //                if (result.Password == passWord)
        //                    return 1;
        //                else
        //                    return -2;
        //            }
        //        }
        //    }
        //}
        public int Login(string userName, string passWord)
        {
            var result = db.Users.SingleOrDefault(a => a.UserName == userName);


            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password != null && result.Password.Equals(passWord, StringComparison.CurrentCultureIgnoreCase))

                        return 1;
                    else
                        return -2;

                }
            }
        }
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
               
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.Phone = entity.Phone;
                //user.GroupID = entity.GroupID;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
        public IEnumerable<Content> ListAllContent()
        {
            IQueryable<Content> model = db.Contents;
            
            return model.OrderByDescending(x => x.CreateDate).ToList();
        }
        public IEnumerable<User> ListAllPaging(string searchString,int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString) || x.Phone.Contains(searchString) || x.Address.Contains(searchString) || x.Email.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page,pageSize);
        }
        //public IEnumerable<UserGroup> ListUserGroup()
        //{
        //    IQueryable<UserGroup> model = db.UserGroups;
        //    return model.ToList();
        //}
        public User GetById(string userName)
        {
            return db.Users.SingleOrDefault(a => a.UserName == userName);
        }
        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }
        //public List<string> GetListCredential(string userName)
        //{
        //    var user = db.Users.Single(x => x.UserName == userName);
        //    var data = (from a in db.Credentials
        //                join b in db.UserGroups on a.UserGroupID equals b.ID
        //                join c in db.Roles on a.RoleID equals c.ID
        //                where b.ID == user.GroupID
        //                select new
        //                {
        //                    RoleID = a.RoleID,
        //                    UserGroupID = a.UserGroupID
        //                }).AsEnumerable().Select(x => new Credential()
        //                {
        //                    RoleID = x.RoleID,
        //                    UserGroupID = x.UserGroupID
        //                });
        //    return data.Select(x => x.RoleID).ToList();

        //}

        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
        public bool ChangeStatus(long id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }
        public bool CheckUserName(string userName)
        {
            return db.Users.Count(x => x.UserName == userName) > 0;
        }
        public bool CheckUserEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }
        
    }
}
