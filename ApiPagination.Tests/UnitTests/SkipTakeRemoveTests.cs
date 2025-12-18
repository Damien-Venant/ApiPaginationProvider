using System.Linq.Expressions;
using ApiPagination.Library;
using ExpressionTreeToString;

namespace ApiPagination.Tests;

public class SkipTakeRemoveTests
{
    [Fact]
    public Task SkipTakeRemove_Should_Remove_Skip()
    {
        // Arrange
        List<int> vals = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        IQueryable<int> query = QueryablePagination.MakePagination((_, _) => vals)
            .Skip(10);

        //Act
        Expression result = SkipTakeRemove.Replace(query.Expression);
        
        //Assert
        return Verify(result.ToString("Textual tree", "C#"));
    }

    [Fact]
    public Task SkipTakeRemove_Should_Remove_Take()
    {
        // Arrange
        List<int> vals = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        IQueryable<int> query = QueryablePagination.MakePagination((_, _) => vals)
            .Take(10);

        //Act
        Expression result = SkipTakeRemove.Replace(query.Expression);
        
        //Assert
        return Verify(result.ToString("Textual tree", "C#"));
    }

    [Fact]
    public Task SkipTakeRemove_Should_Remove_Skip_And_Take()
    {
        // Arrange
        List<int> vals = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        IQueryable<int> query = QueryablePagination.MakePagination((_, _) => vals)
            .Select(pre => pre * 2)
            .Take(10)
            .Skip(10)
            .Where(pre => pre % 2 == 0);

        //Act
        Expression result = SkipTakeRemove.Replace(query.Expression);
        
        //Assert
        return Verify(result.ToString("Textual tree", "C#"));
    }
}