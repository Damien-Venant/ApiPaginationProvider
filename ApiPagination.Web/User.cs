using Bogus;

namespace ApiPagination.Web;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

public class UserFaker : Faker<User>
{
    public UserFaker()
    {
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.FirstName, f => f.Person.FirstName);
        RuleFor(x => x.LastName, f => f.Person.LastName);
    }
}

public class PaginationOffset<T> where T : class
{
    public int NombreElements { get; set; }
    public List<T> Elements { get; set; }
}