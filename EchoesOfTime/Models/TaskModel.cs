namespace EchoesOfTime.Models;

public class TaskModel
{
    public int Id { get; set; } // Primary Key
    public string? Name { get; set; } = string.Empty;
    public TimeSpan ElapsedTime { get; set; } = TimeSpan.Zero;
    public DateTime? ActiveSince { get; set; } = null;
    public string? Details { get; set; } = string.Empty;
    public DateTime Date { get; set; } // Date this task belongs to
}
