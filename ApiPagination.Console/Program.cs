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
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class Program
{
    public static async Task Main()
    {
        HttpClient httpClient = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(15),
        };

        ApiPaginationQuery<User> apiPagination = new ApiPaginationQuery<User>(new ApiPaginationQueryProvider<User>((skip,
            take) =>
        {
            var result = httpClient.GetAsync($"http://localhost:5187/api/users?skip={skip}&take={take}").GetAwaiter()
                .GetResult();
            var content = result.Content.ReadFromJsonAsync<ResultUser>().GetAwaiter().GetResult();
            return content?.Elements;
        }));

        List<int> result = apiPagination
            .Take(500)
            .Select(pre => 10)
            .Skip(100)
            .Where(pre => pre > 1)
            .Skip(10)
            .Where(pre => pre /2 == 0)
            .ToList();

        foreach (var user in result)
        {
            System.Console.WriteLine(user);
        }
    }
}