using System;
using Avalonia.Threading;

namespace SortingAlgorithms;

public static class Stopwatch
{
    private static readonly DispatcherTimer Timer = new();
    private static DateTime _timeStarted;
    public static string TimeElapsed { get; private set; } = "00:00.000";

    static Stopwatch()
    {
        Timer.Interval = TimeSpan.FromMilliseconds(10);
        Timer.Tick += UpdateTime;
    }

    public static void Start()
    {
        _timeStarted = DateTime.Now;
        Timer.Start();
    }

    public static void Stop() => Timer.Stop();
    
    public static void AddTick(EventHandler tick) => Timer.Tick += tick;

    private static void UpdateTime(object? sender, EventArgs e)
    {
        TimeElapsed = (DateTime.Now - _timeStarted).ToString(@"mm\:ss\.fff");
    }
}