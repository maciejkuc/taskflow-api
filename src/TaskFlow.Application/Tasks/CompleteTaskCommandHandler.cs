using Ardalis.Result;
using MediatR;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Tasks;

public class CompleteTaskCommandHandler(ITaskItemRepository taskItemRepository)
    : IRequestHandler<CompleteTaskCommand, Result>
{
    public async Task<Result> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
    {
        var taskItem = await taskItemRepository.GetByIdAsync(request.TaskId, cancellationToken);

        if (taskItem is null)
        {
            return Result.NotFound();
        }

        taskItem.Complete();
        await taskItemRepository.UpdateAsync(taskItem, cancellationToken);

        return Result.Success();
    }
}
