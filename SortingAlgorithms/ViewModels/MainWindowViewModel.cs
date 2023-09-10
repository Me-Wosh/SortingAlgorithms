using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using SortingAlgorithms.Models;
using SortingAlgorithms.Services;

namespace SortingAlgorithms.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly RectangleService _rectangleService = new();
    private int _selectedIndex;
    private double _delayValue = 10d;
    private string _timeElapsed = "00:00.000";
    private bool _algorithmStarted;
    public ObservableCollection<Rectangle> Rectangles { get; set; }
    public ReactiveCommand<Unit, Task> StartSorting { get; set; }
    public ReactiveCommand<Unit, Task> Reset { get; set; }

    public MainWindowViewModel()
    {
        Rectangles = Rects.Rectangles;
        _rectangleService.GenerateRectangles();

        var blockButton = this.WhenAnyValue(
            x => x.AlgorithmStarted, 
            x => x != true);

        StartSorting = ReactiveCommand.Create(async () =>
        {
            AlgorithmStarted = true;
            await Algorithms.SelectedAlgorithm[SelectedIndex](DelayValue);
            AlgorithmStarted = false;
        }, blockButton);
        
        Reset = ReactiveCommand.Create(async () =>
        {
            AlgorithmStarted = true;
            await _rectangleService.ShuffleRectangles(true);
            AlgorithmStarted = false;
        }, blockButton);
        
        Stopwatch.AddTick(UpdateDisplayedTime);
    }

    private bool AlgorithmStarted
    {
        get => _algorithmStarted;
        set => this.RaiseAndSetIfChanged(ref _algorithmStarted, value);
    }
    
    public int SelectedIndex
    {
        get => _selectedIndex; 
        set => this.RaiseAndSetIfChanged(ref _selectedIndex, value);
    }
    public double DelayValue
    {
        get => _delayValue; 
        set => this.RaiseAndSetIfChanged(ref _delayValue, value);
    }

    public string TimeElapsed
    {
        get => _timeElapsed;
        set => this.RaiseAndSetIfChanged(ref _timeElapsed, value);
    }

    private void UpdateDisplayedTime(object? sender, EventArgs args)
    {
        TimeElapsed = (DateTime.Now - Stopwatch.TimeStarted).ToString(@"mm\:ss\.fff");
    }
}