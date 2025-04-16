using System.Security.Cryptography.X509Certificates;
using ToDoListApp.Resources;

namespace ToDoListApp;

public partial class SettingsPage : ContentPage
{	
	public SettingsPage()
	{
		InitializeComponent();
	
	}

    private void OnModeChanged(object sender, ToggledEventArgs e)
    {
		if (SettingManagementService.IsDarkMode) { SettingManagementService.IsDarkMode = false; }
		if (!SettingManagementService.IsDarkMode) { SettingManagementService.IsDarkMode = true; }
    }
}