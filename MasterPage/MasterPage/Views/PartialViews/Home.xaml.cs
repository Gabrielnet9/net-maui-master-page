using MasterPage.ViewModels.PartialViewModels;
using System.Threading.Tasks;

namespace MasterPage.Views.PartialViews;

public partial class Home : ContentView
{
	public Home()
	{
		InitializeComponent();
        this.Loaded += Home_Loaded;
	}

    private async void Home_Loaded(object? sender, EventArgs e)
    {
		var viewmodel = new HomeViewModel();
		await viewmodel.GetMonkeysAsync();
		BindingContext = viewmodel;
    }
}