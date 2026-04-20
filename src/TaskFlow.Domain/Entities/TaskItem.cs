namespace TaskFlow.Domain.Entities;

public class TaskItem
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public bool IsCompleted { get; private set; }
    public int ProjectId { get; private set; }
    public Project? Project { get; private set; }

    private TaskItem()
    {
        Title = string.Empty;
    }

    public TaskItem(string title, int projectId)
    {
        Title = ValidateTitle(title);
        ProjectId = projectId;
    }

    public void Complete()
    {
        IsCompleted = true;
    }

    private static string ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Task title cannot be empty.", nameof(title));
        }

        return title.Trim();
    }
}