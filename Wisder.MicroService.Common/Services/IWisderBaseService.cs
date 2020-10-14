using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wisder.MicroService.Common.Entity;

namespace Wisder.MicroService.Common.Services
{
    public interface IWisderBaseService<TEntity> where TEntity : BaseEntity
    {
        TEntity Get(long id);

        TEntity Get(Expression<Func<TEntity, bool>> exp);

        List<TEntity> GetAll();
        List<TEntity> GetList(Expression<Func<TEntity, bool>> exp);

        TEntity Insert(TEntity entity);
        List<TEntity> Insert(IEnumerable<TEntity> entitys);

        int Update(TEntity entity);
        int Update(IEnumerable<TEntity> entitys);

        TEntity InsertOrUpdate(TEntity entity);

        int Delete(long id);
        int Delete(TEntity entity);
        int Delete(IEnumerable<TEntity> entitys);
        int Delete(Expression<Func<TEntity, bool>> predicate);


        Task<TEntity> GetAsync(long id);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp);

        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> exp);

        Task<TEntity> InsertAsync(TEntity entity);
        Task<List<TEntity>> InsertAsync(IEnumerable<TEntity> entitys);

        Task<int> UpdateAsync(TEntity entity);
        Task<int> UpdateAsync(IEnumerable<TEntity> entitys);

        Task<TEntity> InsertOrUpdateAsync(TEntity entity);

        Task<int> DeleteAsync(long id);
        Task<int> DeleteAsync(TEntity entity);
        Task<int> DeleteAsync(IEnumerable<TEntity> entitys);
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        void Commit();

    }
}
