using MasterPage.Views.PartialViews;

namespace MasterPage.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        var currentTheme = Application.Current!.RequestedTheme;
        ThemeSegmentedControl.SelectedIndex = currentTheme == AppTheme.Light ? 0 : 1;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        LoadInitialView();

        await Task.Delay(2000);

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await UpdatePageAsync(homebtn, new Home());
            selector.IsVisible = true;
        });
    }

    private void SfSegmentedControl_SelectionChanged(object sender, Syncfusion.Maui.Toolkit.SegmentedControl.SelectionChangedEventArgs e)
    {
        Application.Current!.UserAppTheme = e.NewIndex == 0 ? AppTheme.Light : AppTheme.Dark;
    }

    private async void Home_Clicked(object sender, TappedEventArgs e)
    {
        var label = (Label)sender;
        await UpdatePageAsync(label, new Home());
    }

    private async void profile_Clicked(object sender, TappedEventArgs e)
    {
        var label = (Label)sender;
        await UpdatePageAsync(label, new Profile());
    }

    private async void Message_Clicked(object sender, TappedEventArgs e)
    {
        var label = (Label)sender;
        await UpdatePageAsync(label, new Message());
    }

    private async void Settings_Clicked(object sender, TappedEventArgs e)
    {
        var label = (Label)sender;
        await UpdatePageAsync(label, new Settings());
    }

    async Task UpdatePageAsync(Label? currentLabel, ContentView? partialView)
    {
        if (partialView != null)
        {
            await body.Content.FadeTo(0, 100);
            body.Content = partialView;
            body.Content.Opacity = 0;

            ResetNavButton();
            await UpdateUI(currentLabel);

            viewTitle.Text = partialView.GetType().Name;

            await body.Content.FadeTo(1, 100);
        }
        else
        {
            ResetNavButton();
            await UpdateUI(currentLabel);
        }

    }

    async Task UpdateUI(Label? currentLabel)
    {
        if (currentLabel is null)
            return;

        currentLabel.TextColor = Colors.White;

        int currentColumn = (int)currentLabel.GetValue(Grid.ColumnProperty);

        selector.SetValue(Grid.ColumnProperty, currentColumn);

        await selector.ScaleTo(1.2, 150, Easing.BounceIn);
        await selector.ScaleTo(1, 150, Easing.BounceOut);
    }

    void LoadInitialView()
    {
        selector.IsVisible = false;

        ResetNavButton();

        var activity = new ActivityIndicator()
        {
            IsRunning = true,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
        };

        var grid = new Grid();

        grid.Children.Add(activity);

        body.Content = grid;
    }

    void ResetNavButton()
    {
        foreach (var item in footer.Children)
        {
            if (!(item is Label label))
            {
                continue;
            }

            label.TextColor = Color.FromArgb("#212121");
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }
}