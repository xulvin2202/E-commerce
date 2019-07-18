using System.Web.Mvc;

namespace Ecommerce.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_Logout",
                "dang-xuat-admin",
                new { controller = "Login", action = "Logout", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Admin_Login",
                "dang-nhap-admin",
                new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}