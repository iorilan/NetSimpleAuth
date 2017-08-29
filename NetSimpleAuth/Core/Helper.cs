using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Core.Filter;

namespace Core
{
    public static class MapperEx
    {
        public static TTo CreateFrom<TFrom, TTo>(TFrom from)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFrom, TTo>());
            var mapper = config.CreateMapper();
            return mapper.Map<TTo>(from);
        }

        public static void Map<TFrom, TTo>(TFrom from, ref TTo to)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFrom, TTo>());
            var mapper = config.CreateMapper();
            mapper.Map(from, to);
        }
    }

    public static class CookieEx
    {
        public static void SaveInResponseCookie(this HttpCookieCollection cookies, string accessToken, string refreshToken, string loggedInUserId)
        {
            cookies.Set(new HttpCookie(AuthoriseBaseAttribute.ACCESSTOKEN, accessToken));
            cookies.Set(new HttpCookie(AuthoriseBaseAttribute.REFRESHTOKEN, refreshToken));
            cookies.Set(new HttpCookie(AuthoriseBaseAttribute.CURRENT_USERID, loggedInUserId));
        }

        public static bool TryExtractFromRequestCookie(this HttpCookieCollection cookies, out TokenEntity result)
        {
            if (!HttpContext.Current.Request.Cookies.AllKeys.Contains(AuthoriseBaseAttribute.ACCESSTOKEN) ||
                !HttpContext.Current.Request.Cookies.AllKeys.Contains(AuthoriseBaseAttribute.REFRESHTOKEN) ||
                !HttpContext.Current.Request.Cookies.AllKeys.Contains(AuthoriseBaseAttribute.CURRENT_USERID))
            {
                result = TokenEntity.Default();
                return false;
            }

            //// take value from request form
            var accessToken = HttpContext.Current.Request.Cookies[AuthoriseBaseAttribute.ACCESSTOKEN].Value;
            var refreshToken = HttpContext.Current.Request.Cookies[AuthoriseBaseAttribute.REFRESHTOKEN].Value;
            var currentUserId = HttpContext.Current.Request.Cookies[AuthoriseBaseAttribute.CURRENT_USERID].Value;

            result = new TokenEntity(accessToken, refreshToken, DateTime.MinValue, long.Parse(currentUserId));
            return true;
        }
    }
}
