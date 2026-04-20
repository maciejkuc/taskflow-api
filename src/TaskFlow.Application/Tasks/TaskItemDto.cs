namespace TaskFlow.Application.Tasks;

public record TaskItemDto(int Id, string Title, bool IsCompleted, int ProjectId);
