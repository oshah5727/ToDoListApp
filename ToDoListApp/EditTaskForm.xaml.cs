using System.Threading.Tasks;
using SQLite;
using ToDoListApp.Views;

namespace ToDoListApp;

public partial class EditTaskForm : ContentPage
{
    LocalDBService _DBService;
    private TDItem _originalItem;
    public EditTaskForm(TDItem Item)
	{
        _originalItem = Item;
        _DBService = new LocalDBService();
        InitializeComponent();
        NameEntry.Text = Item.Name;
        DescriptionEntry.Text = Item.Description;
        DateEntry.Text = Item.Date;

    }

    private async void CancelButton_Clicked(object sender, EventArgs e)
    {

        Navigation.PushAsync(new DashboardPage());
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        _originalItem.Name = NameEntry.Text;
        _originalItem.Description = DescriptionEntry.Text;
        _originalItem.Date = DateEntry.Text;
        _DBService.Update(_originalItem);
        await Navigation.PopAsync();
        //Navigation.PushAsync(new DashboardPage());

    }
}