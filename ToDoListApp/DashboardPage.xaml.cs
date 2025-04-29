using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using ToDoListApp.Models;
using ToDoListApp.Services;
using ToDoListApp.Views;
using SQLite;
using System.Threading.Tasks;

namespace ToDoListApp.Views

{
    public partial class DashboardPage : ContentPage
    {
        LocalDBService _DBService;
        public List<TDItem> TaskList;
        public DashboardPage()
        {
            InitializeComponent();
            _DBService = new LocalDBService();
            //Task.Run(async () => TaskListDisplay.ItemsSource = await _DBService.GetTDItemsAsync());
            //BindingContext = this;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            LoadTasks();
        }
        private void LoadTasks()
        {
            TaskList = _DBService.GetTDItemsAsync().Result;
            TaskListView.ItemsSource = null;
            TaskListView.ItemsSource = TaskList;
        }

        private async void OnAccountClicked(object sender, EventArgs e)
        {
            // Navigate to account page
            await Navigation.PushAsync(new AccountPage());
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        { await Navigation.PushAsync(new NewTaskForm()); }

        private async void Settings_Clicked(object sender, EventArgs e)
        {
            //Navigate to settings page
            await Navigation.PushAsync(new SettingsPage());
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

        private async void OnEditClicked(object sender, EventArgs e)
        {
            var button = (sender as Button);
            var item = button?.CommandParameter as TDItem;
            if (item != null)
            {
                await Navigation.PushAsync(new EditTaskForm(item));
            }
        }

        //private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        //{


        //}

        private async void DeleteButtonClicked(object sender, EventArgs e)
        {
            var button = (sender as Button);
            var item = button?.CommandParameter as TDItem;
            bool confirm = await DisplayAlert("Delete Task", "Are you sure you want to delete this task?", "Yes", "No");
            //confirm = DisplayAlert("Delete Task", "Are you sure you want to delete this task?", "Yes", "No").Result;
                if (confirm)
                {
                    _DBService.Delete(item);
                    LoadTasks();
                };

        }
    }
}