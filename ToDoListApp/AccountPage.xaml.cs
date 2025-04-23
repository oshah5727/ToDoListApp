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
        _currentUser = await _db.GetUserAsync();

        if (_currentUser != null)
        {
            UsernameEntry.Text = _currentUser.Username;
            EmailEntry.Text = _currentUser.Email;
            PasswordEntry.Text = _currentUser.Password;
        }
        else
        {
            _currentUser = new User(); 
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        _currentUser.Username = UsernameEntry.Text;
        _currentUser.Email = EmailEntry.Text;
        _currentUser.Password = PasswordEntry.Text;

        await _db.SaveUserAsync(_currentUser);
        await DisplayAlert("Saved", "Account information saved.", "OK");
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (_currentUser?.ID != 0)
        {
            await _db.DeleteUserAsync(_currentUser);
            await DisplayAlert("Deleted", "Account has been deleted.", "OK");

            UsernameEntry.Text = string.Empty;
            EmailEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;

            _currentUser = new User();
        }
    }
}