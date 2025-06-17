using CommunityToolkit.Mvvm.ComponentModel;
using MasterPage.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace MasterPage.ViewModels.PartialViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Monkey> monkeys;

        public HomeViewModel()
        {
            Monkeys = [];
        }

        public async Task GetMonkeysAsync()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
            using var reader = new StreamReader(stream);

            var contents = reader.ReadToEnd();

            var list = JsonConvert.DeserializeObject<List<Monkey>>(contents);
            if (list is null)
            {
                return;
            }

            Monkeys = [.. list];
        }
    }
}
