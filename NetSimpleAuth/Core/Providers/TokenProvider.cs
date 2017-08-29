using System;

namespace Core.Providers
{
    // ####################### Run below SQL to create User Tables######

    // #################################################################

    public abstract class TokenProvider
    {
        /// <summary>
        /// Default implementation of generate a new token
        /// </summary>
        /// <returns></returns>
        public virtual TokenEntity CreateNew(long userId)
        {
            var accessToken = GUIDAsToken();
            var refreshToken = GUIDAsToken();
            var expireAt = DateTime.Now.AddMinutes(Setup.TokenExpireMinutes);
            return new TokenEntity(accessToken, refreshToken, expireAt, userId);
        }

        public virtual TokenEntity CreateRefresh()
        {
            var accessToken = GUIDAsToken();
            var refreshToken = GUIDAsToken();
            var expireAt = DateTime.Now.AddMinutes(Setup.TokenExpireMinutes);
            return new TokenEntity(accessToken, refreshToken, expireAt, TokenEntity.DEFAULT_USER_ID);
        }

        public abstract SimpleAuthResult RemoveTokenFor(long userId);

        public abstract RefreshTokenResult UpdateRefresh(string refreshToken);

        public abstract SimpleAuthResult VerifyToken(string accessToken);

        /// <summary>
        /// Default way of create a token
        /// </summary>
        /// <returns></returns>
        private string GUIDAsToken()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            return guid;
        }
    }

}
