using System.Security.Cryptography.X509Certificates;
using ToDoListApp.Views;
using ToDoListApp.Resources;
using SQLite;

namespace ToDoListApp;

public partial class SettingsPage : ContentPage
{	
	public SettingsPage()
	{
		InitializeComponent();
	
	}

    private void OnModeChanged(object sender, EventArgs e)
    {
		if (Application.Current.RequestedTheme.Equals(AppTheme.Light))
		{ Application.Current.UserAppTheme = AppTheme.Dark; }
		else { Application.Current.UserAppTheme = AppTheme.Light; }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new DashboardPage());
    }
}