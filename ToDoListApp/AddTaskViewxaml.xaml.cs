using ToDoListApp.Models;
namespace ToDoListApp;

public partial class AddTaskView : ContentView
{
    public event EventHandler<ToDoItem> TaskAdded;
    public event EventHandler CancelClicked;

    public DateTime MinDate { get; set; } = DateTime.Today;

    public AddTaskView(string taskName)
    {
        InitializeComponent();
        BindingContext = this;
        TaskNameEntry.Text = taskName;
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        CancelClicked?.Invoke(this, EventArgs.Empty);
    }

    private void OnAddTaskClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TaskDescriptionEntry.Text))
        {
            // Display alert for error, in this case handled by the parent page
            var mainPage = Application.Current.MainPage;
            mainPage.DisplayAlert("Error", "Please enter a task description", "OK");
            return;
        }

        var newTask = new ToDoItem
        {
            Name = TaskNameEntry.Text,
            Description = TaskDescriptionEntry.Text,
            DueBy = TaskDueDatePicker.Date.ToString("yyyy-MM-dd"),
            IsComplete = false
        };

        // Raise the event so that the parent page can handle adding the task
        TaskAdded?.Invoke(this, newTask);
    }
}