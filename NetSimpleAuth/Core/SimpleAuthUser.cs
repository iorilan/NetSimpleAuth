using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SimpleAuthUser
    {
        public string UserName { get; set; }
        public string OriginPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        /// <summary>
        /// for mvc model binder
        /// </summary>
        public SimpleAuthUser()
        {

        }

        public SimpleAuthUser(string userName, string originPassword, string email, string phone)
        {
            UserName = userName;
            OriginPassword = originPassword;
            Email = email;
            Phone = phone;
        }
    }
}
