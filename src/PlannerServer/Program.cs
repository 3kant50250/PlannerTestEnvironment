using PlannerServer.Data;
using Microsoft.EntityFrameworkCore;
using PlannerServer.Repositories.Interfaces;
using PlannerServer.Repositories;
using PlannerServer.Services.Interfaces;
using PlannerServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext with SQL Server connection string
builder.Services.AddDbContext<PlannerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories with DI container
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

// Register services with DI container
builder.Services.AddScoped<IStudentService, StudentService>();

// Add controllers
builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed the database
DbSeeder.Seed(app.Services);

// Configure middleware
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PlannerDbContext>();
    db.Database.EnsureCreated(); // Use this if you're not using migrations
    // db.Database.Migrate(); // Use this instead if you are using EF migrations
}

app.MapGet("/students", async (PlannerDbContext db) =>
{
    var students = await db.Students
        .Select(s => new
        {
            s.StudentId,
            s.Name,
            s.Unilogin
        })
        .ToListAsync();

    return Results.Ok(students);
});


app.Run();
