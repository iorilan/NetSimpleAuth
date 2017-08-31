using System.Web;
using System.Web.Mvc;

namespace Core.Filter
{
    /// <summary>
    /// Used to authenticate the api calls
    /// </summary>
    public class ApiAuthorizationAttribute : AuthoriseBaseAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var accessToken = HttpContext.Current.Request[ACCESSTOKEN];
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                filterContext.Result = new JsonResult()
                {
                    Data = SimpleAuthResult.Fail("access token is empty.")
                };
                return;
            }

            var resultToken = _service.TokenLogin(accessToken);
            if (!resultToken.IsSuccess)
            {
                filterContext.Result = new JsonResult()
                {
                    Data = SimpleAuthResult.Fail(resultToken.Error)
                };
            }

            // let it go ,passed
        }
    }
}
