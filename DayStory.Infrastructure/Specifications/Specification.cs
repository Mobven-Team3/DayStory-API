using DayStory.Domain.Entities;
using DayStory.Domain.Specifications;
using System.Linq.Expressions;

namespace DayStory.Infrastructure.Specifications;

public abstract class Specification<TEntity> : ISpecification<TEntity>
    where TEntity : class, IBaseEntity
{
    public abstract Expression<Func<TEntity, bool>> Criteria { get; }
    public virtual Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; } = null;
}
