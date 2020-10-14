using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;
using Wisder.MicroService.Common.Entity;

namespace Wisder.MicroService.UserService.Entitys
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Table(Name = "UserInfo")]
    [Index("idx_userName", "UserName", true)]
    public class UserInfo : BaseEntity
    {
        /// <summary>
        /// 用户登录名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public int UserType { get; set; }
    }
}
