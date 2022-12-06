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
<<<<<<< HEAD
    // �������� ������������ �� id

    // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
    if (user == null) return Results.NotFound(new { message = "������������ �� ������" });

    // ���� ������������ ������, ���������� ���
=======
    // �������� ������������ �� id 

    // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
    if (user == null) return Results.NotFound(new { message = "������������ �� ������" });

    // ���� ������������ ������, ���������� ���
>>>>>>> 1Добавьте файлы проекта.
    return Results.Json(user);
});

app.MapDelete("/api/users/{id}", (string id) =>
{
    using ApplicationContext db = new ApplicationContext();
<<<<<<< HEAD
    // �������� ������������ �� id
    User user = db.Users.FirstOrDefault(u => Convert.ToString(u.Id) == id);

    // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
    if (user == null) return Results.NotFound(new { message = "������������ �� ������" });

    // ���� ������������ ������, ������� ���
=======
    // �������� ������������ �� id
    User user = db.Users.FirstOrDefault(u => Convert.ToString(u.Id) == id);

    // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
    if (user == null) return Results.NotFound(new { message = "������������ �� ������" });

    // ���� ������������ ������, ������� ���
>>>>>>> 1Добавьте файлы проекта.
    db.Users.Remove(user);
    db.SaveChanges();
    return Results.Json(user);
});

app.MapPost("/api/users", (User user) => {
    using ApplicationContext db = new ApplicationContext();

    User user1 = new User();
    db.Users.Add(user1);

<<<<<<< HEAD
    // ��������� ������������ � ������
=======
    // ��������� ������������ � ������
>>>>>>> 1Добавьте файлы проекта.
    db.Users.Add(user);
    db.SaveChanges();
    return user;
});

app.MapPut("/api/users", (User userData) => {
    using ApplicationContext db = new ApplicationContext();
<<<<<<< HEAD
    // �������� ������������ �� id
    var user = db.Users.FirstOrDefault(u => u.Id == userData.Id);
    // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
    if (user == null) return Results.NotFound(new { message = "������������ �� ������" });
    // ���� ������������ ������, �������� ��� ������ � ���������� ������� �������
=======
    // �������� ������������ �� id
    var user = db.Users.FirstOrDefault(u => u.Id == userData.Id);
    // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
    if (user == null) return Results.NotFound(new { message = "������������ �� ������" });
    // ���� ������������ ������, �������� ��� ������ � ���������� ������� �������
>>>>>>> 1Добавьте файлы проекта.

    user.Age = userData.Age;
    user.Name = userData.Name;
    db.SaveChanges();
    return Results.Json(user);
});

app.Run();

public class User
{
    public int Id { get; set; }
<<<<<<< HEAD
    public string Name { get; set; } = ""; // ��� ������������
    public int Age { get; set; } // ������� ������������
=======
    public string Name { get; set; } = ""; // ��� ������������
    public int Age { get; set; } // ������� ������������
>>>>>>> 1Добавьте файлы проекта.
}

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=FirstStep;Username=postgres;Password=12345678");
    }
}