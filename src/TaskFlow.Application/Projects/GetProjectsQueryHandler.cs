using Ardalis.Result;
using MediatR;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Projects;

public class GetProjectsQueryHandler(IProjectRepository projectRepository)
    : IRequestHandler<GetProjectsQuery, Result<IEnumerable<ProjectDto>>>
{
    public async Task<Result<IEnumerable<ProjectDto>>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await projectRepository.GetAllAsync(cancellationToken);

        var dtos = projects.Select(p => new ProjectDto(
            p.Id,
            p.Name,
            p.Description,
            p.CreatedAt,
            p.TaskItems.Count));

        return Result.Success(dtos);
    }
}
