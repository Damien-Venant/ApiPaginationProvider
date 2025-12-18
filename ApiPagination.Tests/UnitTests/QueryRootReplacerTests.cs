using System.Linq.Expressions;
using ApiPagination.Library;
using ExpressionTreeToString;
using Shouldly;

namespace ApiPagination.Tests;

public class QueryRootReplacerTests
{
    
    [Fact]
    public void QueryRootReplace_Should_Replace_Data_Source()
    {
        // Arrange
        List<int> test = [4, 5, 6];
        IQueryable query = QueryablePagination.MakePagination<int>((_, _) => [1, 2, 3, 4, 5]);

        // Assert
        Expression? result = QueryRootReplacer<int>.Replace(query.Expression, test);
        // Act
        Delegate func = Expression.Lambda(result).Compile();
        EnumerableQuery<int>? enumerableQuery = func?.DynamicInvoke() as EnumerableQuery<int>;

        List<int> res = enumerableQuery.ToList();
        res.ShouldBe(test);
    }

    [Fact]
    public void QueryRootReplace_Should_Not_Replace_Data_Source_With_Expression()
    {        // Arrange
        List<int> test = [4, 5, 6];
        List<int> data = [1, 2, 3];
        IQueryable query = data.AsQueryable();

        // Assert
        Expression? result = QueryRootReplacer<int>.Replace(query.Expression, test);
        
        // Act
        Delegate func = Expression.Lambda(result).Compile();
        EnumerableQuery<int> enumertaQuery = (EnumerableQuery<int>)func?.DynamicInvoke();
        var res = enumertaQuery.ToList();
        
        res.ShouldBe(data);
    }
}