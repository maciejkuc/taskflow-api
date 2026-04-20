using Ardalis.Result;
using MediatR;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Tasks;

public class GetTasksByProjectQueryHandler(IProjectRepository projectRepository, ITaskItemRepository taskItemRepository)
    : IRequestHandler<GetTasksByProjectQuery, Result<IEnumerable<TaskItemDto>>>
{
    public async Task<Result<IEnumerable<TaskItemDto>>> Handle(GetTasksByProjectQuery request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        if (project is null)
        {
            return Result.NotFound();
        }

        var taskItems = await taskItemRepository.GetByProjectIdAsync(request.ProjectId, cancellationToken);

        var dtos = taskItems.Select(t => new TaskItemDto(t.Id, t.Title, t.IsCompleted, t.ProjectId)).ToList();

        return Result.Success<IEnumerable<TaskItemDto>>(dtos);
    }
}
