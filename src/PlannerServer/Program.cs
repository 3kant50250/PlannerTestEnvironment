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
builder.Services.AddScoped<ISchoolItemService, SchoolItemService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("http://plannerstestenvironment-client:5001")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddHttpClient("PlannerApi", client =>
{
    client.BaseAddress = new Uri("http://plannerstestenvironment-server:5000/");
});


var app = builder.Build();

// Add seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var context = services.GetRequiredService<PlannerServerContext>();

    logger.LogInformation("Resetting and seeding the database...");

    //  Reset database
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    //  Seed fresh data
    await DbSeeder.SeedAsync(context, logger);
}


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowBlazorClient");

app.Run();
