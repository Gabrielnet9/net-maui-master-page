using MasterPage.ViewModels;

namespace MasterPage.Views;

public partial class OrderPage : ContentPage
{
    private readonly OrderPageViewModel _viewModel;

    int count = 0;
    public OrderPage()
	{
		InitializeComponent();
        _viewModel = new OrderPageViewModel();
        BindingContext = _viewModel;
    }
    private void OnCounterClicked(object? sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}