using System.Windows;
using EchoesOfTime.Database;
using EchoesOfTime.Repositories;
using EchoesOfTime.Services;
using EchoesOfTime.ViewModels;

namespace EchoesOfTime.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Create the DbContext
            var dbContext = new TimeTrackerDbContext();
            DatabaseInitializer.Initialize(dbContext);
            var taskRepository = new TaskRepository(dbContext);
            var taskService = new TaskService(taskRepository);
            DataContext = new MainViewModel(taskService);
        }
    }
}