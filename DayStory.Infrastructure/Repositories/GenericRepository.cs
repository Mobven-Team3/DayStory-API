using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Pagination;
using DayStory.Domain.Repositories;
using DayStory.Domain.Specifications;
using DayStory.Infrastructure.Data.Context;
using DayStory.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DayStory.Infrastructure.Repositories;

public class GenericRepository<TEntity, TContract> : IGenericRepository<TEntity, TContract>
    where TContract : class, IBaseContract
    where TEntity : class, IBaseEntity
{
    private readonly DayStoryAPIDbContext _context;
    private readonly DbSet<TEntity> Table;
    public GenericRepository(DayStoryAPIDbContext context)
    {
        _context = context;
        Table = context.Set<TEntity>();
    }

    public async Task<IQueryable<TEntity>> GetAllAsync()
    {
        var getQuery = Table.AsNoTracking();
        if (getQuery == null)
        {
            throw new ArgumentNullException(typeof(IQueryable<TEntity>).ToString());
        }
        return await Task.FromResult(getQuery);
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        var result = await Table.FindAsync(id);
        return result;
    }

    public async Task<List<TEntity>> FindAsync(ISpecification<TEntity> specification)
    {
        IQueryable<TEntity> query = Table.AsNoTracking();

        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        if (specification.OrderBy != null)
        {
            query = specification.OrderBy(query);
        }

        return await query.ToListAsync();
    }

    public async Task<PagedResponse<TEntity>> GetPagedDataAsync(int pageNumber, int pageSize)
    {
        var totalRecords = await Table.AsNoTracking().CountAsync();
        var totalPages = (int)Math.Ceiling((decimal)totalRecords / (decimal)pageSize);
        if (pageNumber > totalPages)
        {
            pageNumber = totalPages;
        }

        var entities = await Table.AsNoTracking()
            .OrderBy(x => x.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return new PagedResponse<TEntity>(pageSize, pageNumber, totalRecords, totalPages, entities);
    }

    public async Task<bool> AddAsync(TEntity model)
    {
        EntityEntry<TEntity> entityEntry = Table.Add(model);
        try
        {
            await SaveAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

        return entityEntry.State == EntityState.Added;
    }

    public async Task<bool> AddRangeAsync(List<TEntity> datas)
    {
        await Table.AddRangeAsync(datas);
        await SaveAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(TEntity model)
    {
        if (model is IAuditable)
        {
            ((IAuditable)model).UpdatedOn = DateTime.UtcNow;
        }
        EntityEntry<TEntity> entityEntry = Table.Update(model);
        await SaveAsync();
        return entityEntry.State == EntityState.Modified;
    }

    public async Task<bool> UpdateAsync(TContract model)
    {
        var query = Table.AsQueryable();
        var existEntity = await query
            .FirstOrDefaultAsync(x => x.Id == model.Id);

        if (existEntity == null)
            throw new DbUpdateException("Entity not found in database.");

        if (existEntity is IAuditable at)
            at.UpdatedOn = DateTime.UtcNow;

        _context.Entry(existEntity).CurrentValues.SetValues(model);
        await SaveAsync();

        return true;
    }

    public async Task<bool> RemoveAsync(TEntity model)
    {
        if (model is ISoftDelete)
        {
            ((ISoftDelete)model).IsDeleted = true;
            ((ISoftDelete)model).DeletedOn = DateTime.UtcNow;
            return await UpdateAsync(model);
        }
        else
        {
            EntityEntry<TEntity> entityEntry = Table.Remove(model);
            await SaveAsync();
            return entityEntry.State == EntityState.Deleted;
        }
    }

    public async Task<bool> RemoveRangeAsync(List<TEntity> datas)
    {
        foreach (var item in datas)
        {
            await RemoveAsync(item);
        }
        return true;
    }

    public async Task<bool> RemoveByIdAsync(int id)
    {
        var model = await Table.FirstOrDefaultAsync(data => data.Id == id);
        if (model == null)
        {
            throw new ArgumentNullException(typeof(TEntity).ToString());
        }
        return await RemoveAsync(model);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
