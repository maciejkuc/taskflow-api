using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Tasks;

public class CreateTaskCommandHandler(IProjectRepository projectRepository, ITaskItemRepository taskItemRepository, ILogger<CreateTaskCommandHandler> logger)
    : IRequestHandler<CreateTaskCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        if (project is null)
        {
            return Result.NotFound();
        }

        var taskItem = new TaskItem(request.Title, request.ProjectId);
        var created = await taskItemRepository.AddAsync(taskItem, cancellationToken);

        logger.LogInformation("Task created with Id {TaskId} for Project {ProjectId}", created.Id, request.ProjectId);

        return Result.Success(created.Id);
    }
}
