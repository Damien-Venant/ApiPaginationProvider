using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApiPagination.Library
{
    public class ApiPaginationQuery<T> : IQueryable<T>
    {
        public Type ElementType { get; }
        public Expression Expression { get; }
        public IQueryProvider Provider { get; }

        public ApiPaginationQuery(IQueryProvider provider)
        {
            Expression = Expression.Constant(this);
            Provider = provider;
        }

        public ApiPaginationQuery(IQueryProvider provider, Expression expression)
        {
            Provider = provider;
            Expression = expression;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return Provider.Execute<IEnumerable<T>>(Expression)
                .GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}