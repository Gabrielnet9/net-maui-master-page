using MasterPage.ViewModels.PartialViewModels;

namespace MasterPage.Views.PartialViews;

public partial class Profile : ContentView
{
    private readonly ProfileViewModel _viewModel;
    public Profile(ProfileViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}