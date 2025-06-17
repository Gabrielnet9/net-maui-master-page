namespace MasterPage.Views;

public partial class OrderPage : ContentPage
{
    int count = 0;
    public OrderPage()
	{
		InitializeComponent();
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