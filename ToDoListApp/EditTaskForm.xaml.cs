using System.Threading.Tasks;
using SQLite;
using ToDoListApp.Views;

namespace ToDoListApp;

public partial class EditTaskForm : ContentPage
{
    LocalDBService _DBService;
    public EditTaskForm(TDItem Item)
	{
        _DBService = new LocalDBService();
        InitializeComponent();
        NameEntry.Text = Item.Name;
        DescriptionEntry.Text = Item.Description;
        DateEntry.Text = Item.Date;

    }

    //private async void SaveButton_Clicked(object sender, EventArgs e)
    //{
    //    TDItem NewItem = Item;
    //    NewItem.Name = NameEntry.Text;
    //    NewItem.Description = DescriptionEntry.Text;
    //    NewItem.Date = DateEntry.Text;
    //    Task.Run(async () => { await _DBService.Update(NewItem, Item); });
    //    Navigation.PushAsync(new DashboardPage());
    //}

    private async void CancelButton_Clicked(object sender, EventArgs e)
    {

        Navigation.PushAsync(new DashboardPage());
    }

}