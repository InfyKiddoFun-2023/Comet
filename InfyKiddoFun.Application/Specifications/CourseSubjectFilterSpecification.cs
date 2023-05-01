using InfyKiddoFun.Domain.Entities;
using InfyKiddoFun.Domain.Enums;
using InfyKiddoFun.Infrastructure.Specifications.Base;

namespace InfyKiddoFun.Application.Specifications;

public class CourseSubjectFilterSpecification : BaseSpecification<Course>
{
    public CourseSubjectFilterSpecification(Subject? subject)
    {
        if (subject == null)
        {
            FilterCondition = p => true;
        }
        else
        {
            FilterCondition = p => p.Subject == subject;
        }
    }
}