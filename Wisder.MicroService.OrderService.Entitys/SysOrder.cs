using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wisder.MicroService.Common.Entity;

namespace Wisder.MicroService.OrderService.Entitys
{
    /// <summary>
    /// 系统订单表
    /// </summary>
    [Table(Name = "SysOrder")]
    [Index("idx_billNo", "BillNo", false)]
    [Index("idx_plateStatus", "PlateOrderStatus", false)]
    [Index("idx_status", "Status", false)]
    public class SysOrder : BaseEntity
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string BillNo { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(CanUpdate = false)]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 平台订单状态
        /// </summary>
        public int PlateOrderStatus { get; set; }
        /// <summary>
        /// 订单处理状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 付款时间
        /// </summary>
        public DateTime? PayTime { get; set; }
        /// <summary>
        /// 商品明细
        /// </summary>
        [Column(IsIgnore = true)]
        public List<SysOrderItem> ItemList { get; set; }
    }
}
