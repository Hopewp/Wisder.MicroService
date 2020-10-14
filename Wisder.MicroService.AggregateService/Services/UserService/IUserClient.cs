using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wisder.MicroService.Core.MicroClients.Attributes;
using Wisder.MicroService.UserService.Entitys;

namespace Wisder.MicroService.AggregateService.Services.UserService
{
    [MicroClient("http", "UserService")]
    public interface IUserClient
    {
        [GetPath("/Users")]
        public IEnumerable<UserInfo> GetUserList();

        [GetPath("/Users/{Id}")]
        public IEnumerable<UserInfo> GetUserInfo(long Id);
    }
}
