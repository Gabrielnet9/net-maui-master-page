using MasterPage.ViewModels.PartialViewModels;
using MasterPage.Views.PartialViews;

namespace MasterPage.Views;

public partial class MainPage : ContentPage
{
    private readonly HomeViewModel homeViewModel;
    private readonly SettingsViewModel settingsViewModel;
    private readonly ProfileViewModel profileViewModel;
    private readonly MessageViewModel messageViewModel;
    public MainPage()
    {
        InitializeComponent();
        var currentTheme = Application.Current!.RequestedTheme;
        ThemeSegmentedControl.SelectedIndex = currentTheme == AppTheme.Light ? 0 : 1;

        // each partial view needs its own view model
        homeViewModel = new HomeViewModel();
        settingsViewModel = new SettingsViewModel();
        profileViewModel = new ProfileViewModel();
        messageViewModel = new MessageViewModel();
        // feel free to adjust how you will inject your view models.
        // This is mostly a UI project
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadInitialViewAsync();
    }

    private void SfSegmentedControl_SelectionChanged(object sender, Syncfusion.Maui.Toolkit.SegmentedControl.SelectionChangedEventArgs e)
    {
        Application.Current!.UserAppTheme = e.NewIndex == 0 ? AppTheme.Light : AppTheme.Dark;
    }

    private async void Home_Clicked(object sender, TappedEventArgs e)
    {
        var label = (Label)sender;
        await UpdatePageAsync(label, new Home(homeViewModel));
    }

    private async void profile_Clicked(object sender, TappedEventArgs e)
    {
        var label = (Label)sender;
        await UpdatePageAsync(label, new Profile(profileViewModel));
    }

    private async void Message_Clicked(object sender, TappedEventArgs e)
    {
        var label = (Label)sender;
        await UpdatePageAsync(label, new Message(messageViewModel));
    }

    private async void Settings_Clicked(object sender, TappedEventArgs e)
    {
        var label = (Label)sender;
        await UpdatePageAsync(label, new Settings(settingsViewModel));
    }

    async Task UpdatePageAsync(Label? currentLabel, ContentView? partialView)
    {
        SetBodyLoadingState();

        if (partialView != null)
        {
            viewTitle.Text = partialView.GetType().Name;
            ResetNavButton();

            await UpdateUI(currentLabel);

            await body.Content.FadeTo(0, 100);
            body.Content = partialView;
            body.Content.Opacity = 0;

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

        var activeLabelColor = Color.FromArgb("#512BD4");

        currentLabel.TextColor = activeLabelColor;
        int currentColumn = (int)currentLabel.GetValue(Grid.ColumnProperty);

        preSelector.SetValue(Grid.ColumnProperty, currentColumn);

        await UpdateSelector();
    }

    async Task UpdateSelector()
    {
        await Task.Delay(150);

        // Get positions of both BoxViews relative to the grid
        var preSelectorLocation = preSelector.GetBoundsRelativeTo(footer).Location;
        var selectorLocation = selector.GetBoundsRelativeTo(footer).Location;

        // Calculate difference
        double dx = preSelectorLocation.X - selectorLocation.X;
        double dy = preSelectorLocation.Y - selectorLocation.Y;

        // Translate selectot to preSelector's position
        await selector.TranslateTo(dx, dy, 450, Easing.SinInOut);
    }

    async Task LoadInitialViewAsync()
    {
        selector.IsVisible = false;

        ResetNavButton();

        SetBodyLoadingState();

        await UpdatePageAsync(homebtn, new Home(homeViewModel));

        await homeViewModel.GetMonkeysAsync();

        selector.IsVisible = true;
        await UpdateSelector();
    }

    void SetBodyLoadingState()
    {
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

            label.TextColor = Color.FromArgb("#919191");
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }
}