using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Core.Models;
using Core.Providers;

namespace Core
{
    public class SimpleAuthService
    {
        private TokenProvider _provider;
        private readonly bool _isSso;

        public SimpleAuthService(TokenProvider provider)
        {
            _provider = provider;
            _isSso = Setup.IsSSO;
        }



        public SimpleAuthResult<TokenEntity> TokenLoginAndRefresh(string refreshToken)
        {
            var result = _provider.UpdateRefresh(refreshToken);
            if (result.IsSuccess)
            {
                return SimpleAuthResult<TokenEntity>.Success(result.NewToken);
            }
            else
            {
                return SimpleAuthResult<TokenEntity>.Fail(result.Error);
            }
        }
        public SimpleAuthResult TokenLogin(string accessToken)
        {
            var result = _provider.VerifyToken(accessToken);
            if (result.IsSuccess)
            {
                return SimpleAuthResult.Success();
            }
            else
            {
                return SimpleAuthResult.Fail(result.Error);
            }
        }


        public SimpleAuthResult<TokenEntity> UserCrendentialLogin(string username, string password)
        {
            using (var context = new SimpleUserDbContext())
            {
                var tryFindUser = context.LoginUser.FirstOrDefault(x => x.UserName == username);
                if (tryFindUser == null)
                {
                    return SimpleAuthResult<TokenEntity>.Fail(string.Format("user name with '{0}' is not found .", username));
                }

                var hashedPassword = HashPassword(password);
                if (tryFindUser.PasswordHash != hashedPassword)
                {
                    return SimpleAuthResult<TokenEntity>.Fail("user password is not correct");
                }


                if (_isSso)
                {
                    _provider.RemoveTokenFor(tryFindUser.Id);
                }

                ////issue a new token for password login
                var newToken = _provider.CreateNew(tryFindUser.Id);

                return SimpleAuthResult<TokenEntity>.Success(newToken);
            }
        }

        /// <summary>
        /// user registration
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public SimpleAuthResult TryCreateUser(SimpleAuthUser newUser)
        {
            var hashedPassword = HashPassword(newUser.OriginPassword);

            using (var context = new SimpleUserDbContext())
            {
                var userWithUserName = context.LoginUser.FirstOrDefault(x => x.UserName == newUser.UserName);
                if (userWithUserName != null)
                {
                    return SimpleAuthResult.Fail(string.Format("user with name '{0}' already exists", newUser));
                }


                var loginUser = new LoginUser()
                {
                    UserName = newUser.UserName,
                    PasswordHash = hashedPassword,
                    Email = newUser.Email,
                    Phone = newUser.Phone
                };

                context.LoginUser.Add(loginUser);
                context.SaveChanges();

                return SimpleAuthResult.Success();
            }
        }

        private string HashPassword(string originPassword)
        {
            try
            {
                var data = Encoding.ASCII.GetBytes(originPassword);

                var md5 = new MD5CryptoServiceProvider();
                var md5data = md5.ComputeHash(data);

                return Encoding.ASCII.GetString(md5data);
            }
            catch (Exception ex)
            {
                throw new PasswordHashException("Password harshed failed.");
            }
        }
    }
}
