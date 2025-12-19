using System.Linq.Expressions;

namespace ApiPagination.Library;
public class SkipTakeRemove : ExpressionVisitor
{

    public static Expression Replace(Expression expression)
    {
        return (new SkipTakeRemove()).Visit(expression);
    }
    
    protected override Expression VisitMethodCall(MethodCallExpression node)
    {
        if (node.Method.Name == "Take" || node.Method.Name == "Skip")
        {
            return Visit(node.Arguments[0]);
        }
        return base.VisitMethodCall(node);
    }
}