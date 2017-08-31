using System.Web;
using System.Web.Mvc;

namespace Core.Filter
{
    /// <summary>
    /// Used to authenticate the pages
    /// </summary>
    public class CookieBasedAuthorizationAttribute : AuthoriseBaseAttribute
    {
        public const string LoginView = "~/Views/Account/Login.cshtml";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            TokenEntity cookieResult = TokenEntity.Default();

            if (!HttpContext.Current.Request.Cookies.TryExtractFromRequestCookie(out cookieResult))
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = LoginView
                };
                return;
            }

            if (cookieResult.IsEmpty())
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = LoginView
                };
                return;
            }

            var resultToken = _service.TokenLoginAndRefresh(cookieResult.RefreshToken);
            if (!resultToken.IsSuccess)
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = LoginView
                };
            }

            //// update token value in cookie 
            HttpContext.Current.Response.Cookies.SaveInResponseCookie(resultToken.Data.AccessToken, resultToken.Data.RefreshToken, resultToken.Data.UserId.ToString());
        }
    }

}
