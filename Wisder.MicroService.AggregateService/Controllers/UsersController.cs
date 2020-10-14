using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Wisder.MicroService.AggregateService.Dtos.UserService;
using Wisder.MicroService.AggregateService.Pos.UserService;
using Wisder.MicroService.AggregateService.Services.UserService;
using Wisder.MicroService.Common.Exceptions;
using Wisder.MicroService.Core.DynamicMiddleware.Urls;
using Wisder.MicroService.UserService.Entitys;

namespace Wisder.MicroService.AggregateService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IDynamicMiddleUrl _dynamicMiddleUrl;
        private readonly IUserClient _userClient;
        private readonly HttpClient _httpClient;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IDynamicMiddleUrl dynamicMiddleUrl, IUserClient userClient, IHttpClientFactory httpClientFactory, ILogger<UsersController> logger)
        {
            _dynamicMiddleUrl = dynamicMiddleUrl;
            _userClient = userClient;
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;
        }

        [HttpGet("test")]
        public string GetTest()
        {
            return "testOK";
        }

        [HttpPost("Login")]
        public async Task<LoginDto> Login(LoginPo loginPo)
        {
            if (string.IsNullOrWhiteSpace(loginPo?.UserName) || string.IsNullOrWhiteSpace(loginPo.Password))
            {
                throw new BizException("1100", "用户名及密码不能为空");
            }
            string identityUrl = _dynamicMiddleUrl.GetMiddleUrl("http", "IdentityServer");
            //if (identityUrl.EndsWith(":80"))
            //{
            //    identityUrl = identityUrl.Replace(":80", "");
            //}
            //DiscoveryDocumentResponse discoveryDocument = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest() { Address = identityUrl, Policy = new DiscoveryPolicy() { RequireHttps = false } });
            //if (discoveryDocument.IsError)
            //{
            //    Console.WriteLine($"[DiscoveryDocumentResponse Error]: {discoveryDocument.Error}");
            //}

            // 根据用户名和密码建立token
            TokenResponse tokenResponse = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = $"{identityUrl}/connect/token",// discoveryDocument.TokenEndpoint,
                ClientId = "PCMain",
                ClientSecret = "PCMainSecret",
                GrantType = "password",
                UserName = loginPo.UserName,
                Password = loginPo.Password
            });
            if (tokenResponse.IsError)
            {
                throw new BizException(tokenResponse.Error, tokenResponse.ErrorDescription);
            }

            // 获取用户信息
            //UserInfoResponse userInfoResponse = _httpClient.GetUserInfoAsync(new UserInfoRequest()
            //{
            //    Address = discoveryDocument.UserInfoEndpoint,
            //    Token = tokenResponse.AccessToken
            //}).Result;

            // 返回UserDto信息
            LoginDto userDto = new LoginDto();
            //userDto.UserId = Convert.ToInt64(userInfoResponse.Json.TryGetString("sub"));
            userDto.UserName = loginPo.UserName;
            userDto.AccessToken = tokenResponse.AccessToken;
            userDto.ExpiresIn = tokenResponse.ExpiresIn;
            return userDto;
        }

        [HttpGet]
        public IEnumerable<UserInfo> GetUserList()
        {
            return _userClient.GetUserList();
        }

        [HttpGet("{Id}")]
        public IEnumerable<UserInfo> GetUserInfo(long Id)
        {
            return _userClient.GetUserInfo(Id);
        }
    }
}
