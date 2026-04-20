using TaskFlow.Application;
using TaskFlow.Infrastructure;
using TaskFlow.API.Endpoints;
using TaskFlow.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<TaskFlowDbContext>();
	await dbContext.Database.EnsureCreatedAsync();
}

app.UseHttpsRedirection();

app.MapProjectsEndpoints();
app.MapTasksEndpoints();

app.Run();
