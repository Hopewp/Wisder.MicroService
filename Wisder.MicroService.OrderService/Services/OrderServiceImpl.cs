using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wisder.MicroService.Common.Entity;
using Wisder.MicroService.Common.Services;
using Wisder.MicroService.OrderService.Entitys;
using Wisder.MicroService.OrderService.Repositories;

namespace Wisder.MicroService.OrderService.Services
{
    public class OrderServiceImpl : WisderBaseService<SysOrder>, IOrderService
    {
        public OrderServiceImpl(IOrderRepository baseRepository, UnitOfWorkManager unitOfWorkManager, IdBuilder idBuilder)
            : base(baseRepository, unitOfWorkManager, idBuilder)
        {
        }
    }
}
