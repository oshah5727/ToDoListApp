using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using ToDoListApp.Models;
using ToDoListApp.Services;
using SQLite;
namespace ToDoListApp.Views

{
    public partial class DashboardPage : ContentPage
    {
         readonly LocalDBService _DBService;

        public DashboardPage()
        {   
            InitializeComponent();

            //This gets the list with all the items in it
            Task.Run(async () => TaskList.ItemsSource = await _DBService.GetTDItemsAsync());
            





        }



        private async void OnAccountClicked(object sender, EventArgs e)
        {
            // Navigate to account page
            await Navigation.PushAsync(new AccountPage());
        }

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







    }
}