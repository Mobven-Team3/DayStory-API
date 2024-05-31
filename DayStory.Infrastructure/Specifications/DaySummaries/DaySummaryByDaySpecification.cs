using DayStory.Domain.Entities;
using System.Linq.Expressions;

namespace DayStory.Infrastructure.Specifications;

public class DaySummaryByDaySpecification : Specification<DaySummary>
{
    private readonly string _date;
    private readonly int _userId;

    public DaySummaryByDaySpecification(string date, int userId)
    {
        _date = date;
        _userId = userId;
    }

    public override Expression<Func<DaySummary, bool>> Criteria
        => e => e.Date == _date && e.UserId == _userId;
}
