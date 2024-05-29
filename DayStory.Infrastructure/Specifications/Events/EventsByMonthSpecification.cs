using DayStory.Domain.Entities;
using System.Linq.Expressions;

namespace DayStory.Infrastructure.Specifications;

public class EventsByMonthSpecification : Specification<Event>
{
    private readonly string _year;
    private readonly string _month;
    private readonly int _userId;

    public EventsByMonthSpecification(string year, string month, int userId)
    {
        _year = year;
        _month = month;
        _userId = userId;
    }

    public override Expression<Func<Event, bool>> Criteria
    {
        get
        {
            string monthString = int.Parse(_month).ToString("D2");
            return e => e.Date.EndsWith($"-{monthString}-{_year}") && e.UserId == _userId;
        }
    }

    public override Func<IQueryable<Event>, IOrderedQueryable<Event>> OrderBy
        => events => events.OrderBy(e => e.Date)
                           .ThenBy(e => e.Time == null)
                           .ThenBy(e => e.Time);
}
