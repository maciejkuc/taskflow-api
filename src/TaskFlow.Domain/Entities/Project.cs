namespace TaskFlow.Domain.Entities;

public class Project
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public ICollection<TaskItem> TaskItems { get; private set; }

    private Project()
    {
        Name = string.Empty;
        TaskItems = new List<TaskItem>();
    }

    public Project(string name, string? description)
    {
        Name = ValidateName(name);
        Description = description;
        CreatedAt = DateTime.UtcNow;
        TaskItems = new List<TaskItem>();
    }

    public void Update(string name, string? description)
    {
        Name = ValidateName(name);
        Description = description;
    }

    private static string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Project name cannot be empty.", nameof(name));
        }

        return name.Trim();
    }
}