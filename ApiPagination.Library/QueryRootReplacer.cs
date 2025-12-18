using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApiPagination.Library
{
    internal class QueryRootReplacer<T> : ExpressionVisitor
    {
        private IQueryable<T> queryable;
        internal QueryRootReplacer(IEnumerable<T> data)
        {
            queryable = data.AsQueryable();
        }
        internal static Expression Replace(Expression expression, IEnumerable<T> data)
        {
            return (new QueryRootReplacer<T>(data)).Visit(expression);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Value is ApiPaginationQuery<T>)
            {
                return Expression.Constant(queryable);
            }
            return base.VisitConstant(node);
        }
    }
}