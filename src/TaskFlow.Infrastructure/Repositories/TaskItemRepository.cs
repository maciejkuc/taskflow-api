using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Infrastructure.Repositories;

public class TaskItemRepository(TaskFlowDbContext dbContext) : ITaskItemRepository
{
    public async Task<IEnumerable<TaskItem>> GetByProjectIdAsync(int projectId, CancellationToken cancellationToken)
    {
        return await dbContext.TaskItems
            .Where(taskItem => taskItem.ProjectId == projectId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext.TaskItems
            .AsNoTracking()
            .FirstOrDefaultAsync(taskItem => taskItem.Id == id, cancellationToken);
    }

    public async Task<TaskItem> AddAsync(TaskItem taskItem, CancellationToken cancellationToken)
    {
        await dbContext.TaskItems.AddAsync(taskItem, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return taskItem;
    }

    public async Task UpdateAsync(TaskItem taskItem, CancellationToken cancellationToken)
    {
        dbContext.TaskItems.Update(taskItem);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var taskItem = await dbContext.TaskItems.FindAsync([id], cancellationToken);

        if (taskItem is null)
        {
            return;
        }

        dbContext.TaskItems.Remove(taskItem);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}