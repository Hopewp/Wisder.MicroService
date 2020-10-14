using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeSql;
using Wisder.MicroService.Common.Repositories;
using Wisder.MicroService.OrderService.Entitys;

namespace Wisder.MicroService.OrderService.Repositories
{
    public interface IOrderRepository : IWisderBaseRepository<SysOrder>
    {
    }
}
