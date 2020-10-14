using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wisder.MicroService.Common.Entity;
using Wisder.MicroService.Common.Services;
using Wisder.MicroService.UserService.Entitys;
using Wisder.MicroService.UserService.Repositories;

namespace Wisder.MicroService.UserService.Services
{
    public class UserServiceImpl : WisderBaseService<UserInfo>, IUserService
    {
        public UserServiceImpl(IUserRepository baseRepository, UnitOfWorkManager unitOfWorkManager, IdBuilder idBuilder)
               : base(baseRepository, unitOfWorkManager, idBuilder)
        {
        }
    }
}
