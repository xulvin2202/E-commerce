using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Nhập tài khoản và mật khẩu")]
        public string UserName { get; set; }
       
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}