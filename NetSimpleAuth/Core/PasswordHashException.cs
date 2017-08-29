using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class PasswordHashException : Exception
    {
        public string Error { get; set; }
        public PasswordHashException(string message)
        {
            this.Error = message;
        }
    }
}
