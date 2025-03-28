

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiApp1.ViewModel;

public partial class ShowDataViewModel : ObservableObject
{
    [ObservableProperty]
    // List<String> listData=["One", "Two", "Three", "Four", "Five"];
    ObservableCollection<String> listData = ["One", "Two", "Three", "Four", "Five"];

   [ObservableProperty]
    string itemText = "";
    [RelayCommand]
    void Add(string item) { 
        ListData.Add(item);
    }
}