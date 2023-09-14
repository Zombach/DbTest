using DbTest.GrpcService.Contexts;
using DbTest.GrpcService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddDbContext<DbTestContext>(options =>
{
    var configuration = builder.Configuration;
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<SequenceService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
