using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using SortingAlgorithms.Models;
using SortingAlgorithms.Services;

namespace SortingAlgorithms.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly RectangleService _rectangleService = new();
    private double _delayValue = 10d;
    public ObservableCollection<Rectangle> Rectangles { get; set; }
    public ReactiveCommand<Unit, Unit> StartSorting { get; set; }
    public ReactiveCommand<Unit, Unit> Reset { get; set; }

    public MainWindowViewModel()
    {
        Rectangles = Rects.Rectangles;
        _rectangleService.GenerateRectangles();
        StartSorting = ReactiveCommand.Create(() => Algorithms.SelectionSort(DelayValue));
        Reset = ReactiveCommand.Create(_rectangleService.ShuffleRectangles);
    }
    
    public double DelayValue
    {
        get => _delayValue; 
        set => this.RaiseAndSetIfChanged(ref _delayValue, value);
    }
}