using Ardalis.Result;
using MediatR;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Projects;

public class GetProjectByIdQueryHandler(IProjectRepository projectRepository)
    : IRequestHandler<GetProjectByIdQuery, Result<ProjectDto>>
{
    public async Task<Result<ProjectDto>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.Id, cancellationToken);

        if (project is null)
        {
            return Result.NotFound();
        }

        var dto = new ProjectDto(
            project.Id,
            project.Name,
            project.Description,
            project.CreatedAt,
            project.TaskItems.Count);

        return Result.Success(dto);
    }
}
