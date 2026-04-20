using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Interfaces;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetAllAsync(CancellationToken cancellationToken);
    Task<Project?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Project> AddAsync(Project project, CancellationToken cancellationToken);
    Task UpdateAsync(Project project, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}