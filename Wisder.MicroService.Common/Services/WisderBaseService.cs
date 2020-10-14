using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wisder.MicroService.Common.Entity;
using Wisder.MicroService.Common.Repositories;

namespace Wisder.MicroService.Common.Services
{
    public class WisderBaseService<TEntity> : IWisderBaseService<TEntity> where TEntity : BaseEntity
    {
        protected IWisderBaseRepository<TEntity> _baseRepository;
        protected UnitOfWorkManager _unitOfWorkManager;
        protected IdBuilder _idBuilder;
        public WisderBaseService(IWisderBaseRepository<TEntity> baseRepository, UnitOfWorkManager unitOfWorkManager, IdBuilder idBuilder)
        {
            _baseRepository = baseRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _idBuilder = idBuilder;
        }

        public void Commit()
        {
            if (_unitOfWorkManager?.Current != null)
            {
                try
                {
                    _unitOfWorkManager.Current.Commit();
                }
                catch
                {
                    _unitOfWorkManager.Current.Rollback();
                    throw;
                }
                finally
                {
                    _unitOfWorkManager.Current.Dispose();
                }
            }
        }

        public int Delete(long id)
        {
            return _baseRepository.Delete(id);
        }

        public int Delete(TEntity entity)
        {
            return _baseRepository.Delete(entity);
        }

        public int Delete(IEnumerable<TEntity> entitys)
        {
            return _baseRepository.Delete(entitys);
        }

        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            return _baseRepository.Delete(predicate);
        }

        public async Task<int> DeleteAsync(long id)
        {
            return await _baseRepository.DeleteAsync(id);
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            return await _baseRepository.DeleteAsync(entity);
        }

        public async Task<int> DeleteAsync(IEnumerable<TEntity> entitys)
        {
            return await _baseRepository.DeleteAsync(entitys);
        }

        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _baseRepository.DeleteAsync(predicate);
        }

        public TEntity Get(long id)
        {
            return _baseRepository.Get(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> exp)
        {
            return _baseRepository.Where(exp).First(); ;
        }

        public List<TEntity> GetAll()
        {
            return _baseRepository.Select.ToList();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _baseRepository.Select.ToListAsync();
        }

        public async Task<TEntity> GetAsync(long id)
        {
            return await _baseRepository.GetAsync(id);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp)
        {
            return await _baseRepository.Where(exp).FirstAsync();
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> exp)
        {
            return _baseRepository.Where(exp).ToList();
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> exp)
        {
            return await _baseRepository.Where(exp).ToListAsync();
        }

        public TEntity Insert(TEntity entity)
        {
            return _baseRepository.Insert(entity);
        }

        public List<TEntity> Insert(IEnumerable<TEntity> entitys)
        {
            return _baseRepository.Insert(entitys);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            return await _baseRepository.InsertAsync(entity);
        }

        public async Task<List<TEntity>> InsertAsync(IEnumerable<TEntity> entitys)
        {
            return await _baseRepository.InsertAsync(entitys);
        }

        public TEntity InsertOrUpdate(TEntity entity)
        {
            return _baseRepository.InsertOrUpdate(entity);
        }

        public async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            return await _baseRepository.InsertOrUpdateAsync(entity);
        }

        public int Update(TEntity entity)
        {
            return _baseRepository.Update(entity);
        }

        public int Update(IEnumerable<TEntity> entitys)
        {
            return _baseRepository.Update(entitys);
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            return await _baseRepository.UpdateAsync(entity);
        }

        public async Task<int> UpdateAsync(IEnumerable<TEntity> entitys)
        {
            return await _baseRepository.UpdateAsync(entitys);
        }
    }
}
