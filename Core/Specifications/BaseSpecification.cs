using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecification<T> (Expression<Func<T, bool>>? criteria): ISPecification<T>
{

    protected BaseSpecification() : this(null)
    {
    }
    public Expression<Func<T, bool>>? Criteria => criteria;

    public Expression<Func<T, object>> OrderBy {get; private set;}

    public Expression<Func<T, object>> OrderByDescending {get; private set;}

    public bool IsDistinct  {get; private set;}

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpresstion)
    {
        OrderBy = orderByExpresstion;
    }

    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpresstion)
    {
        OrderByDescending = orderByDescExpresstion;
    }

    protected void ApplyDistinct()
    {
        IsDistinct = true;
    }
}

public class BaseSpecification<T, TResult>(Expression<Func<T, bool>>? criteria) :
BaseSpecification<T>(criteria), ISPecification<T, TResult>
{
      protected BaseSpecification() : this(null)
    {
    }
    public Expression<Func<T, TResult>>? Select { get; private set; }

    protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
    {
        Select = selectExpression;
    }
}