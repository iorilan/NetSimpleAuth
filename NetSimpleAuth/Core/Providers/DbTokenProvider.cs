using System;
using System.Linq;
using Core.Models;

namespace Core.Providers
{

    public class DbTokenProvider : TokenProvider
    {
        public override TokenEntity CreateNew(long userId)
        {
            var newToken = base.CreateNew(userId);

            using (var dbContext = new SimpleUserDbContext())
            {
                var obj = MapperEx.CreateFrom<TokenEntity, UserToken>(newToken);
                dbContext.UserToken.Add(obj);
                dbContext.SaveChanges();
            }

            return newToken;
        }

        public override RefreshTokenResult UpdateRefresh(string refreshToken)
        {
            try
            {
                UserToken record;
                using (var context = new SimpleUserDbContext())
                {
                    record = context.UserToken.FirstOrDefault(x => refreshToken == x.RefreshToken);

                    string error = "";
                    if (record == null)
                    {
                        error = string.Format("RefreshToken '{0}' not found", refreshToken);
                        return new RefreshTokenResult(false, error, TokenEntity.Default());
                    }

                    if (record.ExpireAt < DateTime.Now)
                    {
                        error = string.Format("Token '{0}' has been expired.", refreshToken);
                        return new RefreshTokenResult(false, error, TokenEntity.Default());
                    }

                    ////create new one
                    var newToken = CreateRefresh();
                    record.AccessToken = newToken.AccessToken;
                    record.RefreshToken = newToken.RefreshToken;
                    record.ExpireAt = newToken.ExpireAt;
                    context.SaveChanges();

                    return new RefreshTokenResult(true, "", newToken);
                }
            }
            catch (Exception ex)
            {
                var error = string.Format("unknown error .{0}", ex.Message);
                return new RefreshTokenResult(false, error, TokenEntity.Default());
            }

        }

        public override SimpleAuthResult VerifyToken(string accessToken)
        {
            try
            {
                UserToken record;
                using (var context = new SimpleUserDbContext())
                {
                    record = context.UserToken.FirstOrDefault(x => accessToken == x.AccessToken);

                    string error = "";
                    if (record == null)
                    {
                        error = string.Format("AccessToken '{0}' not found", accessToken);
                        return SimpleAuthResult.Fail(error);
                    }

                    if (record.ExpireAt < DateTime.Now)
                    {
                        error = string.Format("Token '{0}' has been expired.", accessToken);
                        return SimpleAuthResult.Fail(error);
                    }

                    return SimpleAuthResult.Success();
                }
            }
            catch (Exception ex)
            {
                var error = string.Format("unknown error .{0}", ex.Message);
                return SimpleAuthResult.Fail(error);
            }
        }
        /// <summary>
        /// If it is single sign on , need to remove the existing token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public override SimpleAuthResult RemoveTokenFor(long userId)
        {
            try
            {
                using (var context = new SimpleUserDbContext())
                {
                    var userTokens = context.UserToken.Where(x => x.UserId == userId).ToList();
                    if (userTokens.Count > 0)
                    {
                        foreach (var userToken in userTokens)
                        {
                            context.UserToken.Remove(userToken);
                            context.SaveChanges();
                        }
                    }

                    return SimpleAuthResult.Success();
                }
            }
            catch (Exception ex)
            {
                var error = string.Format("remove token failed for user '{0}'", userId);
                return SimpleAuthResult.Fail(error);
            }
        }

    }
}
