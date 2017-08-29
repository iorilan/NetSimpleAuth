using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class TokenEntity
    {
        public const long DEFAULT_USER_ID = -1;
        public TokenEntity()
            : this("", "", DateTime.MinValue,DEFAULT_USER_ID)
        {
        }
        public TokenEntity(string accessToken, string refreshToken, DateTime expireAt, long userId)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpireAt = expireAt;
            UserId = userId;
        }

        public static TokenEntity Default()
        {
            return new TokenEntity();
        }

        public bool IsEmpty()
        {
            return string.IsNullOrWhiteSpace(AccessToken) && 
                   string.IsNullOrWhiteSpace(RefreshToken);
        }

        public string AccessToken { get; set; }
        public DateTime ExpireAt { get; set; }
        public string RefreshToken { get; set; }
        public long UserId { get; set; }
    }
}
