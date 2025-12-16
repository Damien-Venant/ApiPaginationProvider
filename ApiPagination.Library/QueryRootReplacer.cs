using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApiPagination.Library
{
    public class QueryRootReplacer<T> : ExpressionVisitor
    {
        private IQueryable<T> queryable;
        public QueryRootReplacer(IEnumerable<T> data)
        {
            queryable = data.AsQueryable();
        }
        public static Expression Replace(Expression expression, IEnumerable<T> data)
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