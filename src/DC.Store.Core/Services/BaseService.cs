using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using static AutoMapper.Mapper;
using DC.Store.Core.Services.Interfaces;
using DC.Store.Data.Repositories.Interfaces;

namespace DC.Store.Core.Services
{
    public abstract class BaseService<TDto, TEntity> : IBaseService<TDto>
        where TDto : class
        where TEntity : class
    {
        #region Fields, Constructors
        protected readonly IBaseRepository<TEntity> _repository;

        internal BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }
        #endregion

        #region GET/Select methods

        /// <summary>
        /// Returns product filtered based on predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<TDto> GetAsync(Expression<Func<TDto, bool>> predicate) =>
            await OnGetAsync(predicate);

        protected async virtual Task<TDto> OnGetAsync(Expression<Func<TDto, bool>> predicate)
        {
            var expression = Map<Expression<Func<TDto, bool>>, Expression<Func<TEntity, bool>>>(predicate);
            return Map<TEntity, TDto>(await _repository.SelectAsync(expression));
        }

        /// <summary>
        /// Returns all prodcuts.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<TDto>> GetAllAsync() =>
            await OnGetAllAsync();

        protected async virtual Task<IList<TDto>> OnGetAllAsync() =>
            Map<IList<TEntity>, IList<TDto>>(await _repository.SelectAllAsync());

        /// <summary>
        /// Returns all prodcuts including Category details.
        /// </summary>
        /// <param name="include"></param>
        /// <returns></returns>
        public async Task<IList<TDto>> GetAllAsync(string include) =>
            await OnGetAllAsync(include);

        protected async virtual Task<IList<TDto>> OnGetAllAsync(string include) =>
            Map<IList<TEntity>, IList<TDto>>(await _repository.SelectAllAsync(include));

        #endregion

        public bool Any(Expression<Func<TDto, bool>> predicate) =>
            OnAny(predicate);

        protected virtual bool OnAny(Expression<Func<TDto, bool>> predicate)
        {
            var expression = Map<Expression<Func<TEntity, bool>>>(predicate);
            return _repository.Any(expression);
        }

        #region Add, Remove, Modify Methods

        public TDto Add(TDto dtoModel) =>
            OnAdd(dtoModel);

        protected virtual TDto OnAdd(TDto dtoModel)
        {
            var entity = _repository.Insert(Map<TDto, TEntity>(dtoModel));
            return Map<TEntity, TDto>(entity);
        }

        public void Modify(TDto dtoModel) =>
            OnModify(dtoModel);

        protected virtual void OnModify(TDto dtoModel) =>
            _repository.Update(Map<TDto, TEntity>(dtoModel));

        public async Task RemoveAsync(params object[] keyValues) =>
            await OnRemove(keyValues);

        protected virtual async Task OnRemove(params object[] keyValues) =>
            await _repository.DeleteAsync(keyValues);

        public void Remove(TDto dtoModel) =>
            OnRemove(dtoModel);

        protected virtual void OnRemove(TDto dtoModel) =>
            _repository.Delete(Map<TDto, TEntity>(dtoModel));

        public async Task SaveChangesAsync() =>
            await OnSaveChangesAsync();

        protected async virtual Task OnSaveChangesAsync() =>
            await _repository.SaveChangesAsync();

        #endregion
    }
}

