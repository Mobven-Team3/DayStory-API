using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Pagination;
using DayStory.Domain.Specifications;

namespace DayStory.Domain.Repositories;

public interface IGenericRepository<TEntity, TContract>
    where TContract : class, IBaseContract
    where TEntity : class, IBaseEntity

{
    Task<bool> AddAsync(TEntity model);
    Task<bool> AddRangeAsync(List<TEntity> datas);
    Task<bool> RemoveAsync(TEntity model);
    Task<bool> RemoveRangeAsync(List<TEntity> datas);
    Task<bool> RemoveByIdAsync(int id);
    Task<bool> UpdateAsync(TEntity model);
    Task<bool> UpdateAsync(TContract model);
    Task SaveAsync();
    Task<IQueryable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task<List<TEntity>> FindAsync(ISpecification<TEntity> specification);
    Task<PagedResponse<TEntity>> GetPagedDataAsync(int pageNumber, int pageSize);
}
