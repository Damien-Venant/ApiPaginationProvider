using ApiPagination.Library;

namespace ApiPagination.Tests;

public class ApiPaginationTests
{
    private readonly List<int> dataSource = Enumerable.Range(1, 10_000).ToList();
    
    private List<int> ServiceGetValues(int take, int skip) =>
        dataSource.Skip(skip).Take(take).ToList();
    
    
    private async Task<List<int>> ServiceGetValuesAsync(int take, int skip)
    {
        await Task.Delay(50);
        return dataSource.Skip(skip).Take(take).ToList();
    }

    
    [Theory]
    [InlineData(0, 10), InlineData(20, 200), InlineData(40, 1000)]
    public Task ApiPagination_Test(int skip, int take)
    {
     //Arrange
     IQueryable<int> apiPagination = QueryablePagination.MakePagination((skipTake) => ServiceGetValues(skipTake.Take, skipTake.Skip));
     //Act
     List<int> result = apiPagination
         .Skip(skip)
         .Take(take)
         .ToList();
     //Assert
     return Verify(result);
    }
    
    [Theory]
    [InlineData(0, 10), InlineData(20, 200), InlineData(40, 1000)]
    public Task ApiPagination_TestAsync(int skip, int take)
    {
        //Arrange
        IQueryable<int> apiPagination = QueryablePagination.MakePagination<int>(async (skipTake) => await ServiceGetValuesAsync(skipTake.Skip, skipTake.Take));
        //Act
        List<int> result = apiPagination
            .Skip(skip)
            .Take(take)
            .ToList();
        //Assert
        return Verify(result);
    }
}