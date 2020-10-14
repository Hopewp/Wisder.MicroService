using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wisder.MicroService.Common.Repositories;
using Wisder.MicroService.UserService.Entitys;

namespace Wisder.MicroService.UserService.Repositories
{
    public interface IUserRepository : IWisderBaseRepository<UserInfo>
    {
    }
}
