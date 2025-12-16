using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace ApiPagination.Library
{
    public class ApiPaginationQueryProvider<T> : IQueryProvider
    {
        private Func<int, int, IEnumerable<T>> apiCall;
        public ApiPaginationQueryProvider(Func<int, int, IEnumerable<T>> apiCall)
        {
            this.apiCall = apiCall;
        }
        
        public IQueryable CreateQuery(Expression expression)
        {
            return new ApiPaginationQuery<T>(this, expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new ApiPaginationQuery<TElement>(this, expression);
        }

        public object Execute(Expression expression)
        {
            ApiPaginationExpressionVisitor visitor = new ApiPaginationExpressionVisitor();
            visitor.Visit(expression);
            
            ApiParameters parameters = visitor.Parameters;
            List<T> data = apiCall(parameters.Skip, parameters.Take).ToList();

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