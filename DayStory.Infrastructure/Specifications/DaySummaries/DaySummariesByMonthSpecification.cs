using DayStory.Domain.Entities;
using System.Linq.Expressions;

namespace DayStory.Infrastructure.Specifications;

public class DaySummariesByMonthSpecification : Specification<DaySummary>
{
    private readonly string _year;
    private readonly string _month;
    private readonly int _userId;

    public DaySummariesByMonthSpecification(string year, string month, int userId)
    {
        _year = year;
        _month = month;
        _userId = userId;
    }

    public override Expression<Func<DaySummary, bool>> Criteria
    {
        get
        {
            string monthString = int.Parse(_month).ToString("D2");
            return e => e.Date.EndsWith($"-{monthString}-{_year}") && e.UserId == _userId;
        }
    }

    public override Func<IQueryable<DaySummary>, IOrderedQueryable<DaySummary>> OrderBy
        => events => events.OrderBy(e => e.Date);
}
