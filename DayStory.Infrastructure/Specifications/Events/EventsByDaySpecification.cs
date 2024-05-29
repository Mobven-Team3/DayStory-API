using DayStory.Domain.Entities;
using System.Linq.Expressions;

namespace DayStory.Infrastructure.Specifications;

public class EventsByDaySpecification : Specification<Event>
{
    private readonly string _date;
    private readonly int _userId;

    public EventsByDaySpecification(string date, int userId)
    {
        _date = date;
        _userId = userId;
    }

    public override Expression<Func<Event, bool>> Criteria
        => e => e.Date == _date && e.UserId == _userId;

    public override Func<IQueryable<Event>, IOrderedQueryable<Event>> OrderBy
        => events => events.OrderBy(e => e.Time == null)
                           .ThenBy(e => e.Time);
}
