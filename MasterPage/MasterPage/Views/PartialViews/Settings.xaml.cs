using MasterPage.ViewModels.PartialViewModels;

namespace MasterPage.Views.PartialViews;

public partial class Settings : ContentView
{
    private readonly SettingsViewModel _viewModel;
    public Settings(SettingsViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}