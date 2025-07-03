using MasterPage.ViewModels.PartialViewModels;
using System.Threading.Tasks;

namespace MasterPage.Views.PartialViews;

public partial class Home : ContentView
{
	private readonly HomeViewModel _viewModel;
    public Home(HomeViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}