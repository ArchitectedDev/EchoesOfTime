using EchoesOfTime.Database;
using EchoesOfTime.Models;
using Microsoft.EntityFrameworkCore;

namespace EchoesOfTime.Repositories;

public class TaskRepository
{
    private readonly TimeTrackerDbContext _dbContext;

    public TaskRepository(TimeTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<TaskModel> GetTasksForDate(DateTime date)
    {
        return _dbContext.Tasks.Where(t => t.Date.Date == date.Date).ToList();
    }

    public void AddTask(TaskModel task)
    {
        _dbContext.Tasks.Add(task);
        _dbContext.SaveChanges();
    }

    public void UpdateTask(TaskModel task)
    {
        var existingTask = _dbContext.Tasks.Local.FirstOrDefault(t => t.Id == task.Id);
        if (existingTask != null)
        {
            // Update the properties manually to avoid tracking conflicts
            _dbContext.Entry(existingTask).CurrentValues.SetValues(task);
        }
        else
        {
            _dbContext.Tasks.Attach(task); // Attach the entity
            _dbContext.Entry(task).State = EntityState.Modified; // Mark it as modified
        }

        _dbContext.SaveChanges();
    }

    public void DeleteTask(TaskModel task)
    {
        // Check if the entity is already being tracked
        var trackedTask = _dbContext.Tasks.Local.FirstOrDefault(t => t.Id == task.Id);

        if (trackedTask != null)
        {
            // Use the tracked instance
            _dbContext.Tasks.Remove(trackedTask);
        }
        else
        {
            // Attach the entity if it's not already tracked
            _dbContext.Tasks.Attach(task);
            _dbContext.Tasks.Remove(task);
        }

        _dbContext.SaveChanges();
    }
}
