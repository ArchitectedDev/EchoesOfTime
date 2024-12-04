using System.Windows.Input;
using System.Windows.Threading;
using EchoesOfTime.Commands;
using EchoesOfTime.Models;

namespace EchoesOfTime.ViewModels;

public class TaskViewModel : BaseViewModel
{
    private readonly DispatcherTimer _timer;

    public int Id { get; set; } // Task identifier
    private string _name;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public event EventHandler ActivationChanged;
    private DateTime? _activeSince;
    public DateTime? ActiveSince
    {
        get => _activeSince;
        set
        {
            if (SetProperty(ref _activeSince, value))
            {
                OnPropertyChanged(nameof(IsActive));
                OnPropertyChanged(nameof(TotalElapsedTime));
                ActivationChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public event EventHandler ElapsedTimeChanged;
    private TimeSpan _elapsedTime;
    public TimeSpan ElapsedTime
    {
        get => _elapsedTime;
        set
        {
            SetProperty(ref _elapsedTime, value);
            NotifyElapsedTimeChanged();
        }
    }

    private string _details;
    public string Details
    {
        get => _details;
        set => SetProperty(ref _details, value);
    }

    public bool IsActive => ActiveSince.HasValue;

    public TimeSpan TotalElapsedTime =>
        ElapsedTime + (ActiveSince.HasValue ? DateTime.Now - ActiveSince.Value : TimeSpan.Zero);

    private void NotifyElapsedTimeChanged()
    {
        OnPropertyChanged(nameof(TotalElapsedTime));
        ElapsedTimeChanged?.Invoke(this, EventArgs.Empty);
    }

    private void StartStop()
    {
        if (IsActive)
        {
            Deactivate();
        }
        else
        {
            Activate();
        }
    }

    public void Activate()
    {
        if (!IsActive)
        {
            ActiveSince = DateTime.Now;
            StartTimer();
        }
    }

    public void Deactivate()
    {
        if (IsActive)
        {
            StopTimer();
            ElapsedTime += DateTime.Now - ActiveSince.Value;
            ActiveSince = null;
            OnPropertyChanged(nameof(IsActive));
        }
    }

    private bool _isExpanded;
    public bool IsExpanded
    {
        get => _isExpanded;
        set => SetProperty(ref _isExpanded, value);
    }

    private DateTime _date;
    public DateTime Date
    {
        get => _date;
        set => SetProperty(ref _date, value);
    }

    public TaskModel ToModel()
    {
        return new TaskModel
        {
            Id = Id,
            Name = Name,
            ElapsedTime = ElapsedTime,
            ActiveSince = ActiveSince,
            Details = Details,
            Date = Date
        };
    }

    // Commands
    public ICommand IncrementTimeCommand { get; }
    public ICommand DecrementTimeCommand { get; }
    public ICommand DeleteTaskCommand { get; }
    public ICommand StartStopCommand { get; }
    public ICommand ExpandCollapseTaskCommand { get; }

    // Base Constructor
    public TaskViewModel()
    {
        // Initialize Timer
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += OnTimerTick;

        // Initialize commands
        IncrementTimeCommand = new RelayCommand(IncrementTime);
        DecrementTimeCommand = new RelayCommand(DecrementTime);
        StartStopCommand = new RelayCommand(StartStop);
        DeleteTaskCommand = new RelayCommand(DeleteTask);
        ExpandCollapseTaskCommand = new RelayCommand(() => IsExpanded = !IsExpanded);
    }

    // Constructor for new tasks
    public TaskViewModel(DateTime date)
        : this()
    {
        Id = new Random().Next(); // Generate a unique ID for simplicity (replace with actual logic).
        Name = string.Empty;
        ElapsedTime = TimeSpan.Zero;
        Details = string.Empty;
        IsExpanded = false;
        Date = date;
    }

    // Constructor for loading existing tasks
    public TaskViewModel(TaskModel taskModel)
        : this()
    {
        Id = taskModel.Id;
        Name = taskModel.Name;
        ElapsedTime = taskModel.ElapsedTime;
        ActiveSince = taskModel.ActiveSince;
        Details = taskModel.Details;
        IsExpanded = false;
        Date = taskModel.Date;

        if (ActiveSince != null)
        {
            StartTimer();
        }
    }

    // Timer Tick Handler
    private void OnTimerTick(object sender, EventArgs e)
    {
        NotifyElapsedTimeChanged();
    }

    // Timer Control Methods
    private void StartTimer()
    {
        _timer.Start();
    }

    private void StopTimer()
    {
        _timer.Stop();
    }

    // Command Handlers
    private void IncrementTime()
    {
        ElapsedTime += TimeSpan.FromMinutes(5);
    }

    private void DecrementTime()
    {
        ElapsedTime -= TimeSpan.FromMinutes(5);
        if (ElapsedTime < TimeSpan.Zero)
        {
            ElapsedTime = TimeSpan.Zero;
        }
    }

    public event EventHandler TaskDeleted;
    private void DeleteTask()
    {
        TaskDeleted?.Invoke(this, EventArgs.Empty);
    }
}
