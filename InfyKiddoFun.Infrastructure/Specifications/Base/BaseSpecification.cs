using System.Linq.Expressions;

namespace InfyKiddoFun.Infrastructure.Specifications.Base;

public class BaseSpecification<T> : ISpecification<T> where T : class
{
    public Expression<Func<T, bool>> FilterCondition { get; set; }

    public List<Expression<Func<T, object>>> Includes { get; } = new();

    public List<string> IncludeStrings { get; } = new();

    protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected virtual void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }
}