using MasterPage.ViewModels.PartialViewModels;

namespace MasterPage.Views.PartialViews;

public partial class Message : ContentView
{
    private readonly MessageViewModel _viewModel;
    public Message(MessageViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}