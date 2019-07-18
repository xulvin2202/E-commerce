using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Login
    {
        [Key]
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Cần nhập tài khoản và mật khẩu")]
        public string UserName { get; set; }

   
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
    }
}