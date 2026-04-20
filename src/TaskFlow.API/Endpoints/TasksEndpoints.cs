using Ardalis.Result;
using MediatR;
using TaskFlow.Application.Tasks;

namespace TaskFlow.API.Endpoints;

public static class TasksEndpoints
{
    public static IEndpointRouteBuilder MapTasksEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/projects/{projectId:int}/tasks", async (int projectId, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetTasksByProjectQuery(projectId), cancellationToken);
            return result.Status switch
            {
                ResultStatus.NotFound => Results.NotFound(),
                ResultStatus.Ok => Results.Ok(result.Value),
                _ => Results.StatusCode(500)
            };
        });

        app.MapPost("/api/projects/{projectId:int}/tasks", async (int projectId, CreateTaskRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateTaskCommand(projectId, request.Title);
            var result = await sender.Send(command, cancellationToken);
            return result.Status switch
            {
                ResultStatus.Ok => Results.Created($"/api/projects/{projectId}/tasks/{result.Value}", result.Value),
                ResultStatus.NotFound => Results.NotFound(),
                ResultStatus.Invalid => Results.ValidationProblem(
                    result.ValidationErrors.ToDictionary(e => e.Identifier, e => new[] { e.ErrorMessage })),
                _ => Results.StatusCode(500)
            };
        });

        app.MapPut("/api/tasks/{id:int}/complete", async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new CompleteTaskCommand(id), cancellationToken);
            return result.Status switch
            {
                ResultStatus.NotFound => Results.NotFound(),
                ResultStatus.Ok => Results.Ok(),
                _ => Results.StatusCode(500)
            };
        });

        return app;
    }
}

public record CreateTaskRequest(string Title);
