using System.Linq.Expressions;

namespace InfyKiddoFun.Infrastructure.Specifications.Base;

public interface ISpecification<T> where T : class
{
    Expression<Func<T, bool>> FilterCondition { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
}