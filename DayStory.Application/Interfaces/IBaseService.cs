using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Pagination;

namespace DayStory.Application.Interfaces;

public interface IBaseService<TEntity, TContract>
    where TEntity : class, IBaseEntity
    where TContract : class, IBaseContract
{
    Task AddAsync(TContract model);
    Task AddRangeAsync(List<TContract> datas);
    Task RemoveAsync(TContract model);
    Task RemoveRangeAsync(List<TContract> datas);
    Task RemoveByIdAsync(int id);
    Task UpdateAsync(TContract model);
    Task<List<TContract>> GetAllAsync();
    Task<TContract> GetByIdAsync(int id);
    Task<PagedResponse<TContract>> GetPagedDataAsync(int pageNumber, int pageSize);
}
