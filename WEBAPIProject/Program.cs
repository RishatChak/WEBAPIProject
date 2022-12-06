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
    // получаем пользователя по id

    // если не найден, отправляем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });

    // если пользователь найден, отправляем его
    return Results.Json(user);
});

app.MapDelete("/api/users/{id}", (string id) =>
{
    using ApplicationContext db = new ApplicationContext();
    // получаем пользователя по id
    User user = db.Users.FirstOrDefault(u => Convert.ToString(u.Id) == id);

    // если не найден, отправляем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });

    // если пользователь найден, удаляем его
    db.Users.Remove(user);
    db.SaveChanges();
    return Results.Json(user);
});

app.MapPost("/api/users", (User user) => {
    using ApplicationContext db = new ApplicationContext();

    User user1 = new User();
    db.Users.Add(user1);

    // добавляем пользователя в список
    db.Users.Add(user);
    db.SaveChanges();
    return user;
});

app.MapPut("/api/users", (User userData) => {
    using ApplicationContext db = new ApplicationContext();
    // получаем пользователя по id
    var user = db.Users.FirstOrDefault(u => u.Id == userData.Id);
    // если не найден, отправляем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });
    // если пользователь найден, изменяем его данные и отправляем обратно клиенту

    user.Age = userData.Age;
    user.Name = userData.Name;
    db.SaveChanges();
    return Results.Json(user);
});

app.Run();

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = ""; // имя пользователя
    public int Age { get; set; } // возраст пользователя
}

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=FirstStep;Username=postgres;Password=12345678");
    }
}