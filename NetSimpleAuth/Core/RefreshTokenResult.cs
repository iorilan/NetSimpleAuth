using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{

    public class RefreshTokenResult
    {
        public RefreshTokenResult(bool isSuccess, string error, TokenEntity newToken)
        {
            IsSuccess = isSuccess;
            Error = error;
            NewToken = newToken;
        }


        public string Error { get; set; }
        public bool IsSuccess { get; set; }
        public TokenEntity NewToken { get; set; }
    }

}
