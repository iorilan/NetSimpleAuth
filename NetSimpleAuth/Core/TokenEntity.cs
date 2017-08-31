using System;

namespace Core
{
    public class TokenEntity
    {
        public const long DEFAULT_USER_ID = -1;
        private const string EXPIRYDATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        public TokenEntity()
            : this("", "", DateTime.MinValue,DEFAULT_USER_ID)
        {
        }
        public TokenEntity(string accessToken, string refreshToken, DateTime expireAt, long userId)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpireAt = expireAt.ToString(EXPIRYDATE_FORMAT);
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

        public string ExpireAt { get; set; }

        public string RefreshToken { get; set; }

        public long UserId { get; set; }
    }
}
