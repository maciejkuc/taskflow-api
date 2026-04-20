using TaskFlow.Application;
using TaskFlow.Infrastructure;
using TaskFlow.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapProjectsEndpoints();
app.MapTasksEndpoints();

app.Run();
