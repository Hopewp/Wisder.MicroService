using FreeSql;
using System;
using System.Collections.Generic;
using System.Text;
using Wisder.MicroService.Common.Entity;

namespace Wisder.MicroService.Common.Repositories
{
    public interface IWisderBaseRepository<TEntity> : IBaseRepository<TEntity, long> where TEntity : BaseEntity
    {
    }
}
