using ApiPagination.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/api/users", (int skip = 0, int take = 50) =>
{
    UserFaker userFaker = new UserFaker();
    List<User> users = userFaker.Generate(take);

    return new PaginationOffset<User>()
    {
        NombreElements = users.Count,
        Elements = users
    };
});

app.Run();