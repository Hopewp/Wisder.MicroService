using FreeSql;
using System;
using System.Collections.Generic;
using System.Text;
using Wisder.MicroService.Common.Entity;

namespace Wisder.MicroService.Common.Repositories
{
    public class WisderBaseRepository<TEntity> : BaseRepository<TEntity, long>, IWisderBaseRepository<TEntity> where TEntity : BaseEntity
    {
        public WisderBaseRepository(IFreeSql fsql) : base(fsql, null, null)
        {

        }
    }
}
