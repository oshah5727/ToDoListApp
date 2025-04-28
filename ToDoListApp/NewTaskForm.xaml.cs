using System.Threading.Tasks;
using SQLite;
using ToDoListApp.Views;

namespace ToDoListApp;

public partial class NewTaskForm : ContentPage
{
    LocalDBService _DBService;
    public NewTaskForm()
	{
        _DBService = new LocalDBService();
        InitializeComponent();

	}

    private async Task Button_Clicked(object sender, EventArgs e)
    {
        TDItem CreatedItem = new TDItem();
        CreatedItem.Name = NameEntry.Text;
        CreatedItem.Description = DescriptionEntry.Text;
        CreatedItem.Date = DateEntry.Text;
        Task.Run(async () => { await _DBService.Create(CreatedItem); });
        Navigation.PushAsync(new DashboardPage());
    }
}