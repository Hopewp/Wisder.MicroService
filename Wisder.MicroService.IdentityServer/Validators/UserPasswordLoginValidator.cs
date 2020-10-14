//using Dapper;
using IdentityModel;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Wisder.MicroService.IdentityServer.Domain;

namespace Wisder.MicroService.IdentityServer.Validators
{
    public class UserPasswordLoginValidator : IResourceOwnerPasswordValidator
    {
        //private IDbConnection _dbConnection;
        private IFreeSql _freeSql;
        //public UserPasswordLoginValidator(IDbConnection dbConnection, IFreeSql freeSql)
        public UserPasswordLoginValidator(IFreeSql freeSql)
        {
            //_dbConnection = dbConnection;
            _freeSql = freeSql;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            //var userInfo = await _dbConnection.QueryFirstOrDefaultAsync<UserInfo>("select * from users where UserName=@UserName", new { context.UserName });
            var userName = context.UserName;
            var userInfo = await RedisHelper.CacheShellAsync($"identity_loginuser_{userName}", 60, async () =>
                 {
                     var userInfo = await _freeSql.Select<UserInfo>().Where(p => p.UserName == userName).FirstAsync(p => new UserInfo { Id = p.Id, UserName = p.UserName, Password = p.Password, Email = p.Email });
                     return userInfo;
                 });

            if (userInfo == null)
            {
                context.Result = new GrantValidationResult()
                {
                    IsError = true,
                    Error = "用户不存在"
                };
            }
            else if (userInfo.Password != context.Password)
            {
                context.Result = new GrantValidationResult()
                {
                    IsError = true,
                    Error = "密码不正确"
                };
            }
            else
            {
                context.Result = new GrantValidationResult(userInfo.Id.ToString(), IdentityModel.OidcConstants.AuthenticationMethods.Password, GetUserClaims(userInfo));
            }
        }
        private Claim[] GetUserClaims(UserInfo user)
        {
            return new Claim[]
            {
                new Claim(JwtClaimTypes.Subject, user.Id.ToString() ?? ""),
                new Claim(JwtClaimTypes.Id, user.Id.ToString() ?? ""),
                new Claim(JwtClaimTypes.Name, user.UserName?? ""),
                new Claim(JwtClaimTypes.Email, user.Email?? "")
            };
        }
    }
}
