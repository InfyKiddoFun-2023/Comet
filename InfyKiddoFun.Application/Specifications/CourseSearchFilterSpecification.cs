using InfyKiddoFun.Domain.Entities;
using InfyKiddoFun.Infrastructure.Specifications.Base;

namespace InfyKiddoFun.Application.Specifications;

public class CourseSearchFilterSpecification : BaseSpecification<Course>
{
    public CourseSearchFilterSpecification(string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
        {
            FilterCondition = p => true;
        }
        else
        {
            searchString = searchString.Trim().ToLower();
            FilterCondition = p => p.Title.ToLower().Contains(searchString);
        }
    }
}