using InfyKiddoFun.Domain.Entities;
using InfyKiddoFun.Domain.Enums;
using InfyKiddoFun.Infrastructure.Specifications.Base;

namespace InfyKiddoFun.Application.Specifications;

public class CourseAgeGroupFilterSpecification : BaseSpecification<Course>
{
    public CourseAgeGroupFilterSpecification(AgeGroup? ageGroup)
    {
        if (ageGroup == null)
        {
            FilterCondition = p => true;
        }
        else
        {
            FilterCondition = p => p.AgeGroup == ageGroup;
        }
    }
}