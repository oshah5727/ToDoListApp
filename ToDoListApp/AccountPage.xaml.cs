using ToDoListApp.Models;

namespace ToDoListApp;

public partial class AccountPage : ContentPage
{
    private LocalDBService _db;
    private User _currentUser;

    public AccountPage()
    {
        InitializeComponent();
        _db = new LocalDBService();
        LoadUser();
    }

    private async void LoadUser()
    {
        try
        {
            _currentUser = await _db.GetUserAsync();

            if (_currentUser == null)
            {
               
                _currentUser = new User();
            }

            CurrentUsername.Text = _currentUser.Username ?? "Default Username";
            CurrentEmail.Text = _currentUser.Email ?? "Default Email";
            CurrentPassword.Text = _currentUser.Password != null
                ? new string('*', _currentUser.Password.Length)
                : "***";
            ProfilePicture.Source = string.IsNullOrEmpty(_currentUser.ProfilePicturePath)
                ? "profile_placeholder.png"
                : _currentUser.ProfilePicturePath;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load user data: {ex.Message}", "OK");
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(UsernameEntry.Text))
            {
                _currentUser.Username = UsernameEntry.Text;
            }
            if (!string.IsNullOrEmpty(EmailEntry.Text))
            {
                _currentUser.Email = EmailEntry.Text;
            }

            if (!string.IsNullOrEmpty(PasswordEntry.Text))
            {
                _currentUser.Password = PasswordEntry.Text;
            }

            await _db.SaveUserAsync(_currentUser); 
            await DisplayAlert("Saved", "Account information updated.", "OK");
            LoadUser(); 
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to save user data: {ex.Message}", "OK");
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        try
        {
            bool confirm = await DisplayAlert("Delete Account", "Are you sure you want to delete your account?", "Yes", "No");
            if (confirm)
            {
                if (_currentUser?.ID != 0)
                {
                    await _db.DeleteUserAsync(_currentUser);
                    await DisplayAlert("Deleted", "Your account has been deleted.", "OK");

                    _currentUser = new User();
                    LoadUser();
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to delete user account: {ex.Message}", "OK");
        }
    }

    private async void OnChangePictureClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select a Profile Picture"
            });

            if (result != null)
            {
                _currentUser.ProfilePicturePath = result.FullPath;
                ProfilePicture.Source = result.FullPath; 
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to update profile picture: {ex.Message}", "OK");
        }
    }
}
