using System;
using System.Linq.Expressions;

namespace ApiPagination.Library
{
    internal class ApiPaginationExpressionVisitor : ExpressionVisitor
    {
        internal ApiParameters Parameters { get; } = new ApiParameters();
        
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == "Skip")
                Parameters.Skip = GetConstantesValues(node.Arguments[1]);

            if (node.Method.Name == "Take")
                Parameters.Take = GetConstantesValues(node.Arguments[1]);
            return base.VisitMethodCall(node);
        }

        private int GetConstantesValues(Expression expression)
        {
            if (expression.NodeType != ExpressionType.Constant)
                throw new Exception();

            ConstantExpression constantExpression = (ConstantExpression)expression;
            return (int)constantExpression.Value;
        }
    }
}