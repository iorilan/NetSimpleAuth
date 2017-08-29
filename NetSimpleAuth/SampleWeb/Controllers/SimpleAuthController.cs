using System;
using System.Web;
using System.Web.Mvc;
using Core;
using Core.Filter;
using Core.Models;
using Core.Providers;

namespace SampleWeb.Controllers
{
    
    public class SimpleAuthController : Controller
    {

        private SimpleAuthService _service;
        public SimpleAuthController()
        {
            _service = new SimpleAuthService(new DbTokenProvider());
        }

        [CookieBasedAuthorization()]
        // GET: SimpleAuth
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            var loginResult = _service.UserCrendentialLogin(userName, password);
            if (!loginResult.IsSuccess)
            {
                ViewBag.ErrorMessage = loginResult.Error;
                return View("Login");
            }
            else
            {
                Response.Cookies.SaveInResponseCookie(loginResult.Data.AccessToken, loginResult.Data.RefreshToken, loginResult.Data.UserId.ToString());

                return RedirectToAction("Index", "Home");
            }
        }

        
        [HttpPost]
        public ActionResult ApiLogin(string userName, string password)
        {
            var ret = _service.UserCrendentialLogin(userName, password);
            if (!ret.IsSuccess)
            {
                return Json(ret.Error);
            }
            else
            {
                return Json(ret.Data);
            }
        }

        [HttpPost]
        public ActionResult ApiRefreshToken(string refreshToken, long userId)
        {
            var ret = _service.TokenLoginAndRefresh(refreshToken);
            if (!ret.IsSuccess)
            {
                return Json(ret.Error);
            }
            else
            {
                return Json(ret.Data);
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(SimpleAuthUser userInfo)
        {
            var ret = _service.TryCreateUser(userInfo);
            if (!ret.IsSuccess)
            {
                return View("Register");
            }

            return RedirectToAction("Index", "Home");
        }


        [ApiAuthorization]
        public ActionResult SampleApi()
        {
            return Json("ok");
        }
    }
}