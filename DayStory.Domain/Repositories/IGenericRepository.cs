using DayStory.Domain.Entities;
using DayStory.Domain.Pagination;

namespace DayStory.Domain.Repositories;

public interface IGenericRepository<T> where T : class, IBaseEntity
{
    Task<bool> AddAsync(T model);
    Task<bool> AddRangeAsync(List<T> datas);
    Task<bool> RemoveAsync(T model);
    Task<bool> RemoveRangeAsync(List<T> datas);
    Task<bool> RemoveByIdAsync(int id);
    Task<bool> UpdateAsync(T model);
    Task SaveAsync();
    Task<IQueryable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<PagedResponse<T>> GetPagedDataAsync(int pageNumber, int pageSize);
}
