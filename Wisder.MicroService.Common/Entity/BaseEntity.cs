using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wisder.MicroService.Common.Entity
{
    public class BaseEntity
    {
        [Column(CanUpdate = false, IsNullable = false, IsPrimary = true, Name = "Id", Position = 1)]
        public long Id { get; set; }
    }
}
