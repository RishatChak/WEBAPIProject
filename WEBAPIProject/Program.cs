using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

using ApplicationContext db = new ApplicationContext();

app.MapGet("/api/users", () => db.Users);

app.MapGet("/api/users/{id}", (string id) =>
{
    using ApplicationContext db = new ApplicationContext();
    User user = db.Users.FirstOrDefault(u => Convert.ToString(u.Id) == id);

    if (user == null) return Results.NotFound(new { message = "������������ �� ������" });
    if (user == null) return Results.NotFound(new { message = "" });

    return Results.Json(user);
});

app.MapDelete("/api/users/{id}", (string id) =>
{
    using ApplicationContext db = new ApplicationContext();

    User user = db.Users.FirstOrDefault(u => Convert.ToString(u.Id) == id);

    if (user == null) return Results.NotFound(new { message = "������������ �� ������" });
   
    if (user == null) return Results.NotFound(new { message = "" });
        
    db.Users.Remove(user);
    db.SaveChanges();
    return Results.Json(user);
});

app.MapPost("/api/users", (User user) => {
    using ApplicationContext db = new ApplicationContext();

    User user1 = new User();
    db.Users.Add(user1);

    db.Users.Add(user);
    db.SaveChanges();
    return user;
});

app.MapPut("/api/users", (User userData) => {
    using ApplicationContext db = new ApplicationContext();

    var user = db.Users.FirstOrDefault(u => u.Id == userData.Id);

    if (user == null) return Results.NotFound(new { message = "������������ �� ������" });
    if (user == null) return Results.NotFound(new { message = "" });

    user.Age = userData.Age;
    user.Name = userData.Name;
    db.SaveChanges();
    return Results.Json(user);
});

app.Run();

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }

}

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=FirstStep;Username=postgres;Password=12345678");
    }
}