using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using ToDoListApp.Models;
using ToDoListApp.Services;
namespace ToDoListApp.Views

{
    public partial class DashboardPage : ContentPage
    {
        private DashboardViewModel _viewModel;

        public DashboardPage()
        {
            InitializeComponent();
            _viewModel = new DashboardViewModel();
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadTasksAsync();
        }

        private void OnAccountClicked(object sender, EventArgs e)
        {
            // Navigate to account page
            await Navigation.PushAsync(new AccountPage());
        }

        private async void OnSignOutClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Sign Out", "Are you sure you want to sign out?", "Yes", "No");

            if (confirm)
            {
        
                // Navigate to login page
                await Navigation.PushAsync(new LoginPage());
            }
        }

        private async void OnTaskCompletionChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.BindingContext is ToDoItem task)
            {
                task.IsComplete = e.Value;
                await _viewModel.UpdateTaskAsync(task);
            }
        }

        private async void OnTaskOptionsClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is string taskName)
            {
                string action = await DisplayActionSheet("Task Options", "Cancel", null, "Edit", "Delete");

                switch (action)
                {
                    //case "Edit":
                    //    await Navigation.PushAsync(new EditTaskPage(taskName));
                    //    break;
                    case "Delete":
                        bool confirm = await DisplayAlert("Delete Task", "Are you sure you want to delete this task?", "Yes", "No");
                        if (confirm)
                        {
                            await _viewModel.DeleteTaskAsync(taskName);
                        }
                        break;
                }
            }
        }

    }

    public class DashboardViewModel : BindableObject
    {
        private ObservableCollection<ToDoItem> _todayTasks;
        private ObservableCollection<ToDoItem> _upcomingTasks;
        private int _completedTasksCount;
        private int _pendingTasksCount;

        // Service for data operations
        private readonly IToDoService _todoService;

        public DashboardViewModel()
        {
            // Initialize collections
            TodayTasks = new ObservableCollection<ToDoItem>();
            UpcomingTasks = new ObservableCollection<ToDoItem>();

            // Initialize service - in a real app, this would be injected
            _todoService = new ToDoService();

            // Initialize commands
            QuickAddCommand = new Command(ExecuteQuickAdd);
        }

        public ObservableCollection<ToDoItem> TodayTasks
        {
            get => _todayTasks;
            set
            {
                _todayTasks = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ToDoItem> UpcomingTasks
        {
            get => _upcomingTasks;
            set
            {
                _upcomingTasks = value;
                OnPropertyChanged();
            }
        }

        public int CompletedTasksCount
        {
            get => _completedTasksCount;
            set
            {
                _completedTasksCount = value;
                OnPropertyChanged();
            }
        }

        public int PendingTasksCount
        {
            get => _pendingTasksCount;
            set
            {
                _pendingTasksCount = value;
                OnPropertyChanged();
            }
        }

        public ICommand QuickAddCommand { get; private set; }

        private async void ExecuteQuickAdd()
        {
            // Get the value from QuickAddEntry
            var entry = App.Current.MainPage?.FindByName("QuickAddEntry") as Entry;

            if (entry != null && !string.IsNullOrWhiteSpace(entry.Text))
            {
                var newTask = new ToDoItem
                {
                    Name = entry.Text,
                    Description = "Quick added task",
                    DueBy = DateTime.Today.ToString("yyyy-MM-dd"),
                    IsComplete = false
                };

                await _todoService.AddTaskAsync(newTask);

                // Clear the entry
                entry.Text = string.Empty;

                // Refresh data
                await LoadTasksAsync();
            }
        }

        public async Task LoadTasksAsync()
        {
            try
            {
                // Get all tasks
                var allTasks = await _todoService.GetTasksAsync();

                // Filter tasks for today
                var today = DateTime.Today.ToString("yyyy-MM-dd");
                var todayTasks = allTasks.Where(t => t.DueBy == today).ToList();

                // Filter upcoming tasks (next 7 days, excluding today)
                var upcomingTasks = allTasks
                    .Where(t => {
                        if (DateTime.TryParse(t.DueBy, out DateTime dueDate))
                        {
                            return dueDate > DateTime.Today && dueDate <= DateTime.Today.AddDays(7);
                        }
                        return false;
                    })
                    .OrderBy(t => t.DueBy)
                    .ToList();

                // Update observable collections
                TodayTasks.Clear();
                foreach (var task in todayTasks)
                {
                    TodayTasks.Add(task);
                }

                UpcomingTasks.Clear();
                foreach (var task in upcomingTasks)
                {
                    UpcomingTasks.Add(task);
                }

                // Update statistics
                CompletedTasksCount = allTasks.Count(t => t.IsComplete);
                PendingTasksCount = allTasks.Count(t => !t.IsComplete);
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, display alert, etc.)
                Debug.WriteLine($"Error loading tasks: {ex.Message}");
                await App.Current.MainPage.DisplayAlert("Error", "Failed to load tasks. Please try again.", "OK");
            }
        }

        public async Task UpdateTaskAsync(ToDoItem task)
        {
            await _todoService.UpdateTaskAsync(task);

            // Update statistics after task update
            CompletedTasksCount = TodayTasks.Count(t => t.IsComplete) + UpcomingTasks.Count(t => t.IsComplete);
            PendingTasksCount = TodayTasks.Count(t => !t.IsComplete) + UpcomingTasks.Count(t => !t.IsComplete);
        }

        public async Task DeleteTaskAsync(string taskName)
        {
            await _todoService.DeleteTaskAsync(taskName);

            // Remove from collections
            var todayTask = TodayTasks.FirstOrDefault(t => t.Name == taskName);
            if (todayTask != null)
            {
                TodayTasks.Remove(todayTask);
            }

            var upcomingTask = UpcomingTasks.FirstOrDefault(t => t.Name == taskName);
            if (upcomingTask != null)
            {
                UpcomingTasks.Remove(upcomingTask);
            }

            // Update statistics
            CompletedTasksCount = TodayTasks.Count(t => t.IsComplete) + UpcomingTasks.Count(t => t.IsComplete);
            PendingTasksCount = TodayTasks.Count(t => !t.IsComplete) + UpcomingTasks.Count(t => !t.IsComplete);
        }
    }

    // This is just a placeholder for the converter mentioned in XAML
    public class BoolToStrikethroughConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool isCompleted && isCompleted)
            {
                return TextDecorations.Strikethrough;
            }
            return TextDecorations.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}