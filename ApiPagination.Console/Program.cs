using System.Net.Http.Json;
using ApiPagination.Library;

namespace ApiPagination.Console;

public class ResultUser
{
    public int NombreElements { get; set; }
    public User[] Elements { get; set; }
}
public class User
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}

public class Program
{
    public static async Task Main()
    {
        HttpClient httpClient = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(15),
        };

        IQueryable<User> apiPagination = QueryablePagination.MakePagination<User>(async (skiptTake) =>
        {
            var result = await httpClient.GetAsync($"http://localhost:5187/api/users?skip={skiptTake.Skip}&take={skiptTake.Take}");
            var content = await result.Content.ReadFromJsonAsync<ResultUser>();
            return content?.Elements;
        });


        for(int i = 0; i < 100_000; ++i)
        {
            List<string> result = apiPagination
                .Take(1)
                .Skip(1 * i)
                .Select(pre => pre.FirstName + " " + pre.LastName)
                .ToList();
            
            result.ForEach(System.Console.WriteLine);
        }
    }
}