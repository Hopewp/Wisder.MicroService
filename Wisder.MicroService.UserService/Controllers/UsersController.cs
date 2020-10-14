using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Wisder.MicroService.UserService.Entitys;
using Wisder.MicroService.UserService.Services;

namespace Wisder.MicroService.UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;
        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<UserInfo>> GetUserList()
        {
            return await _userService.GetAllAsync();
        }

        [HttpGet("{Id}")]
        public async Task<UserInfo> GetUserInfo(long Id)
        {
            return await _userService.GetAsync(Id);
        }
    }
}
