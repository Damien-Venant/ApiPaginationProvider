using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ApiPagination.Tests")]
namespace ApiPagination.Library
{
    internal class ApiPaginationQuery<T> : IQueryable<T>
    {
        public Type ElementType { get; }
        public Expression Expression { get; }
        public IQueryProvider Provider { get; }

        internal ApiPaginationQuery(IQueryProvider provider)
        {
            Expression = Expression.Constant(this);
            Provider = provider;
        }

        internal ApiPaginationQuery(IQueryProvider provider, Expression expression)
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