using Microsoft.Maui.Controls;

namespace LoginPage
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
            }
            else
            {
                DisplayAlert("Login Failed", "Invalid username or password", "OK");
            }
        }
    }
}