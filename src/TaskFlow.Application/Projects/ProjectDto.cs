namespace TaskFlow.Application.Projects;

public record ProjectDto(int Id, string Name, string? Description, DateTime CreatedAt, int TaskCount);
