using System.Collections.ObjectModel;
using System.Windows.Input;
using EchoesOfTime.Commands;
using System.ComponentModel;
using EchoesOfTime.Repositories;

namespace EchoesOfTime.ViewModels;

public class MainViewModel : BaseViewModel
{
    private TaskViewModel _currentActiveTask;
    public TaskViewModel CurrentActiveTask
    {
        get => _currentActiveTask;
        set
        {
            _currentActiveTask?.Deactivate();

            SetProperty(ref _currentActiveTask, value);

            _currentActiveTask?.Activate();

        }
    }

    private DateTime _viewingDate = DateTime.Today;
    public DateTime ViewingDate
    {
        get => _viewingDate;
        set
        {
            if (_viewingDate != value)
            {
                SetProperty(ref _viewingDate, value);
                LoadTasksForDate(_viewingDate);
            }
        }
    }

    private readonly TaskRepository _taskRepository;

    public ObservableCollection<TaskViewModel> Tasks { get; set; } = [];

    public TimeSpan TotalElapsedTime
    {
        get => Tasks.Aggregate(TimeSpan.Zero, (sum, task) => sum + task.TotalElapsedTime);
    }

    // Commands
    public ICommand NewTaskCommand { get; }
    public ICommand PreviousDayCommand { get; }
    public ICommand NextDayCommand { get; }
    public ICommand GotoActiveTaskCommand { get; }

    public MainViewModel(TaskRepository taskRepository)
    {
        _taskRepository = taskRepository;

        // Initialize commands
        NewTaskCommand = new RelayCommand(AddNewTask);
        PreviousDayCommand = new RelayCommand(() => ViewingDate = ViewingDate.AddDays(-1));
        NextDayCommand = new RelayCommand(() => ViewingDate = ViewingDate.AddDays(1));
        GotoActiveTaskCommand = new RelayCommand(() =>
        {
            if (CurrentActiveTask != null)
            {
                ViewingDate = CurrentActiveTask.Date;
            }
        });

        // Load tasks for today by default
        LoadTasksForDate(ViewingDate);
    }

    public void LoadTasksForDate(DateTime date)
    {
        // Clear the current tasks
        Tasks.Clear();

        var taskModels = _taskRepository.GetTasksForDate(date);
        foreach (var taskModel in taskModels)
        {
            var task = new TaskViewModel(taskModel);
            InitialiseTask(task);

            if (task.IsActive && CurrentActiveTask == null)
            {
                CurrentActiveTask = task; // Restore the active task
            }
        }

        OnPropertyChanged(nameof(TotalElapsedTime));
    }

    private void AddNewTask()
    {
        var task = new TaskViewModel(ViewingDate);
        var taskModel = task.ToModel();
        _taskRepository.AddTask(taskModel);

        task.Id = taskModel.Id; // Sync generated ID
        InitialiseTask(task);

        OnPropertyChanged(nameof(TotalElapsedTime));
    }

    private void InitialiseTask(TaskViewModel task)
    {
        task.PropertyChanged += OnTaskPropertyChanged;
        task.ActivationChanged += OnTaskActivationChanged;
        task.ElapsedTimeChanged += OnElapsedTimeChanged;
        task.TaskDeleted += OnTaskDeleted;
        Tasks.Add(task);
    }

    private void OnTaskActivationChanged(object sender, EventArgs e)
    {
        if (sender is TaskViewModel task)
        {
            _taskRepository.UpdateTask(task.ToModel());
            CurrentActiveTask = task.IsActive ? task : null;
        }
    }

    private void OnElapsedTimeChanged(object sender, EventArgs e)
    {
        if (sender is TaskViewModel task && task.Date.Date == ViewingDate.Date)
        {
            OnPropertyChanged(nameof(TotalElapsedTime));

            _taskRepository.UpdateTask(task.ToModel());
        }
    }

    private void OnTaskPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (!(sender is TaskViewModel task) || e.PropertyName == "TotalElapsedTime")
        {
            return;
        }

        _taskRepository.UpdateTask(task.ToModel());
    }

    private void OnTaskDeleted(object sender, EventArgs e)
    {
        if (sender is TaskViewModel task)
        {
            // Unsubscribe from events
            task.PropertyChanged -= OnTaskPropertyChanged;
            task.ActivationChanged -= OnTaskActivationChanged;
            task.ElapsedTimeChanged -= OnElapsedTimeChanged;
            task.TaskDeleted -= OnTaskDeleted;

            _taskRepository.DeleteTask(task.ToModel());
            Tasks.Remove(task);

            OnPropertyChanged(nameof(TotalElapsedTime));
        }
    }
}
