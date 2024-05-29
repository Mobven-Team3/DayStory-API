using DayStory.Domain.Entities;
using System.Linq.Expressions;

namespace DayStory.Domain.Specifications;

public interface ISpecification<TEntity> where TEntity : class, IBaseEntity
{
    Expression<Func<TEntity, bool>> Criteria { get; }
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; }
}
