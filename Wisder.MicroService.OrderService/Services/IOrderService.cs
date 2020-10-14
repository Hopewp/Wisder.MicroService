using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wisder.MicroService.Common.Services;
using Wisder.MicroService.OrderService.Entitys;

namespace Wisder.MicroService.OrderService.Services
{
    public interface IOrderService : IWisderBaseService<SysOrder>
    {
    }
}
