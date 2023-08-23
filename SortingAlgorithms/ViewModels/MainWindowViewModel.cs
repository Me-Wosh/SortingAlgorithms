using System;
using System.Collections.ObjectModel;
using ReactiveUI;
using Rectangle = SortingAlgorithms.DataModels.Rectangle;

namespace SortingAlgorithms.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string? _selectedItem;
    public ObservableCollection<Rectangle> Rectangles { get; set; } = new ();

    public MainWindowViewModel()
    {
        var random = new Random();

        for (var i = 0; i < 50; i++)
        {
            var randomValue = random.Next(10, 600);
            
            Rectangles.Add(new Rectangle
            {
                Height = randomValue
            });
        }
    }

    public string SelectedItem
    {
        get => _selectedItem;
        set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
    }
}