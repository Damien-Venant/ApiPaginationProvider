using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("ApiPagination.Tests")]
namespace ApiPagination.Library
{
    public struct SkipTake
    {
        public int Skip { get; }
        public int Take { get; }

        internal SkipTake(int skip, int take)
        {
            this.Skip = skip;
            this.Take = take;
        }
    }
    internal abstract class BaseApiPaginationQueryPagination<T> : IQueryProvider
    {
        public IQueryable CreateQuery(Expression expression)
            => new ApiPaginationQuery<object>(this, expression);

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
            => new ApiPaginationQuery<TElement>(this, expression);

        protected abstract IEnumerable<T> getData(SkipTake skipTake);

        public object Execute(Expression expression)
        {
            ApiPaginationExpressionVisitor visitor = new ApiPaginationExpressionVisitor();
            visitor.Visit(expression);
            
            ApiParameters parameters = visitor.Parameters;
            List<T> data = getData(new SkipTake(parameters.Skip, parameters.Take))
                .ToList();

            var newExpression = QueryRootReplacer<T>.Replace(expression, data);
            newExpression = SkipTakeRemove.Replace(newExpression);

            var d = Expression.Lambda(newExpression).Compile();
            return d.DynamicInvoke(); 
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return (TResult)Execute(expression);
        }
    }
}