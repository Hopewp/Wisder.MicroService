using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wisder.MicroService.Common.Entity;

namespace Wisder.MicroService.OrderService.Entitys
{
    /// <summary>
    /// 订单商品明细表
    /// </summary>
    [Table(Name = "SysOrderItem")]
    [Index("idx_orderId", "OrderId", false)]
    [Index("idx_skuId", "SkuId", false)]
    public class SysOrderItem : BaseEntity
    {
        [Column(CanUpdate = false, Position = 2)]
        public long OrderId { get; set; }
        public int Seq { get; set; }
        public long SkuId { get; set; }
        [Column(Precision = 18, Scale = 2)]
        public decimal Qty { get; set; }
    }
}
