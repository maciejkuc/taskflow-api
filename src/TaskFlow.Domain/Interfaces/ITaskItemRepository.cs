using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Interfaces;

public interface ITaskItemRepository
{
    Task<IEnumerable<TaskItem>> GetByProjectIdAsync(int projectId, CancellationToken cancellationToken);
    Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<TaskItem> AddAsync(TaskItem taskItem, CancellationToken cancellationToken);
    Task UpdateAsync(TaskItem taskItem, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}