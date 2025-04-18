using Microsoft.Maui.Controls;
using ToDoListApp.Views;

namespace ToDoListApp
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            if (username == "admin" && password == "password123")
            {
                DisplayAlert("Login Successful", "Welcome!", "OK");
                // Navigate to the main page of the app
                Application.Current.MainPage = new NavigationPage(new DashboardPage());
            }
            else
            {
                DisplayAlert("Login Failed", "Invalid username or password", "OK");
            }
        }
    }
}