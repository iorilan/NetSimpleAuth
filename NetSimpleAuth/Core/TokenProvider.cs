using System;
using System.Collections.Generic;

namespace Core
{
    // ####################### Run below SQL to create User Tables######

    



    // #################################################################

    public abstract class TokenProvider
    {
        public static volatile int ExpireInMintes = 60; // default is 60 minites

        /// <summary>
        /// Default implementation of generate a new token
        /// </summary>
        /// <returns></returns>
        public virtual TokenEntity GenerateNew()
        {
            var accessToken = GUIDAsToken();
            var refreshToken = GUIDAsToken();
            var expireAt = DateTime.Now.AddMinutes(ExpireInMintes);
            return new TokenEntity(accessToken, refreshToken, expireAt);
        }

        public abstract RefreshTokenResult TryRefresh(string refreshToken);

        private string GUIDAsToken()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            return guid;
        }
    }

    public class MemoryTokenProvider : TokenProvider
    {
        public Dictionary<string, TokenEntity> RefreshTokens { get; set; }
        public object __tokenlock = new object();
        public override TokenEntity GenerateNew()
        {
            var token = base.GenerateNew();
            lock (__tokenlock)
            {
                RefreshTokens.Add(token.RefreshToken, token);
            }

            return token;
        }

        public override RefreshTokenResult TryRefresh(string refreshToken)
        {
            try
            {
                string error = "";
                if (!RefreshTokens.ContainsKey(refreshToken))
                {
                    error = string.Format("RefreshToken '{0}' not found", refreshToken);
                    return new RefreshTokenResult(false, error, TokenEntity.Default());
                }

                var token = RefreshTokens[refreshToken];
                if (token.ExpireAt > DateTime.Now)
                {
                    error = string.Format("Token '{0}' has been expired.", refreshToken);
                    return new RefreshTokenResult(false, error, TokenEntity.Default());
                }

                ////create new one
                var newToken = GenerateNew();

                ////remove old one
                lock (__tokenlock)
                {
                    RefreshTokens.Remove(refreshToken);
                }

                return new RefreshTokenResult(true, "", newToken);
            }
            catch (Exception ex)
            {
                var error = string.Format("unknown error .{0}", ex.Message);
                return new RefreshTokenResult(false, error, TokenEntity.Default());
            }

        }
    }

    public class DbTokenProvider : TokenProvider
    {
        public override TokenEntity GenerateNew()
        {
            throw new NotImplementedException();
        }

        public override RefreshTokenResult TryRefresh(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }

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

    public class TokenService
    {
        private TokenProvider _provider;

        public TokenService(TokenProvider provider)
        {
            _provider = provider;
        }

        public bool Validate(string refreshToken, out TokenEntity newToken)
        {
            throw new NotImplementedException();
        }

        public bool Validate(string username, string password, out TokenEntity newToken)
        {
            throw new NotImplementedException();
        }
    }

    public class TokenEntity
    {
        public TokenEntity(string accessToken, string refreshToken, DateTime expireAt)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpireAt = expireAt;
        }

        public static TokenEntity Default()
        {
            return new TokenEntity("", "", DateTime.MinValue);
        }
        public string AccessToken { get; set; }
        public DateTime ExpireAt { get; set; }
        public string RefreshToken { get; set; }
    }
}
