using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisder.MicroService.AggregateService.Dtos.UserService
{
    public class LoginDto
    {
        public string AccessToken { set; get; }
        public int ExpiresIn { set; get; }
        public string UserName { set; get; }
        public long UserId { set; get; }
    }
}
