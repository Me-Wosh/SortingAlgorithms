using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using Rectangle = SortingAlgorithms.DataModels.Rectangle;

namespace SortingAlgorithms.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string? _selectedItem;
    public ObservableCollection<Rectangle> Rectangles { get; set; } = new ();
    public ReactiveCommand<Unit, Unit> StartSorting { get; set; }

    public MainWindowViewModel()
    {
        var random = new Random();

        for (var i = 0; i < 70; i++)
        {
            var randomValue = random.Next(10, 600);
            
            Rectangles.Add(new Rectangle(Height: randomValue));
        }
        
        StartSorting = ReactiveCommand.Create(BubbleSort);
    }

    public string SelectedItem
    {
        get => _selectedItem;
        set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
    }
    
    public async void BubbleSort()
    {
        var active = true;

        while (active)
        {
            active = false;

            for (var i = 0; i < Rectangles.Count - 1; i++)
            {
                if (Rectangles[i + 1].Height >= Rectangles[i].Height)
                    continue;
                
                (Rectangles[i + 1], Rectangles[i]) = (Rectangles[i], Rectangles[i + 1]);
                active = true;
                
                await Task.Delay(TimeSpan.FromMilliseconds(5));
            }
        }
    }
}