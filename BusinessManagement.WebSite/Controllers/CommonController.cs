

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace BusinessManagement.WebSite.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;

    public class CommonController : Controller
    {
        public ActionResult SetCulture(string culture)
        {
            string referer = Request.Headers["Referer"].ToString();
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            if (referer == "")
            {
                return Redirect(Url.Content("~/"));
            }

            return Redirect(referer);
        }
    }
}
