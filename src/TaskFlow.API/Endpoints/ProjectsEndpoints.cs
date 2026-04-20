using Ardalis.Result;
using MediatR;
using TaskFlow.Application.Projects;

namespace TaskFlow.API.Endpoints;

public static class ProjectsEndpoints
{
    public static IEndpointRouteBuilder MapProjectsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/projects");

        group.MapGet("/", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetProjectsQuery(), cancellationToken);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.StatusCode(500);
        });

        group.MapGet("/{id:int}", async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetProjectByIdQuery(id), cancellationToken);
            return result.Status switch
            {
                ResultStatus.NotFound => Results.NotFound(),
                ResultStatus.Ok => Results.Ok(result.Value),
                _ => Results.StatusCode(500)
            };
        });

        group.MapPost("/", async (CreateProjectCommand command, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(command, cancellationToken);
            return result.Status switch
            {
                ResultStatus.Ok => Results.Created($"/api/projects/{result.Value}", result.Value),
                ResultStatus.Invalid => Results.ValidationProblem(
                    result.ValidationErrors.ToDictionary(e => e.Identifier, e => new[] { e.ErrorMessage })),
                _ => Results.StatusCode(500)
            };
        });

        return app;
    }
}
