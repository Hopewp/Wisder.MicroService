using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Wisder.MicroService.Common.Repositories;
using Wisder.MicroService.OrderService.Entitys;

namespace Wisder.MicroService.OrderService.Repositories
{
    public class OrderRepository : WisderBaseRepository<SysOrder>, IOrderRepository
    {
        public OrderRepository(IFreeSql fsql) : base(fsql)
        {

        }
    }
}
