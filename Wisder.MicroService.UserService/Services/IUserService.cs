using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wisder.MicroService.Common.Services;
using Wisder.MicroService.UserService.Entitys;

namespace Wisder.MicroService.UserService.Services
{
    public interface IUserService : IWisderBaseService<UserInfo>
    {
    }
}
