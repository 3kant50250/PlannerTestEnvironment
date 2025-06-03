using PlannerServer.Data;
using Microsoft.EntityFrameworkCore;
using PlannerServer.Repositories.Interfaces;
using PlannerServer.Repositories;
using PlannerServer.Services.Interfaces;
using PlannerServer.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext with SQL Server connection string
builder.Services.AddDbContext<PlannerServerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PlannerServerContext") ?? throw new InvalidOperationException("Connection string 'PlannerServerContext' not found.")));

// Register dependencies
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80); // Important: listens on container port 80
});

var app = builder.Build();

// Add seed data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PlannerServerContext>();

    db.Database.EnsureDeleted();
    db.Database.EnsureCreated(); // or db.Database.Migrate(); if using migrations

    DbSeeder.Seed(db); // Your custom method to add data if necessary
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
