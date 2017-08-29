using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Core.Providers;

namespace Core.Filter
{
    public class AuthoriseBaseAttribute : ActionFilterAttribute
    {
        public const string ACCESSTOKEN = "accesstoken";
        public const string REFRESHTOKEN = "refreshtoken";
        public const string CURRENT_USERID = "SimpleAuth_CurrentUser";

        protected SimpleAuthService _service;

        public AuthoriseBaseAttribute()
        {
            _service = new SimpleAuthService(new DbTokenProvider());
        }
        
    }
}
