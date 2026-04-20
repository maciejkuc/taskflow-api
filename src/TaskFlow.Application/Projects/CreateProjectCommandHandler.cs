using Ardalis.Result;
using MediatR;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Projects;

public class CreateProjectCommandHandler(IProjectRepository projectRepository)
    : IRequestHandler<CreateProjectCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project(request.Name, request.Description);
        var created = await projectRepository.AddAsync(project, cancellationToken);

        return Result.Success(created.Id);
    }
}
