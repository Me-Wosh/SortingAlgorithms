using System;
using Avalonia.Threading;

namespace SortingAlgorithms;

public static class Stopwatch
{
    private static readonly DispatcherTimer Timer = new();
    public static DateTime TimeStarted { get; private set; }

    static Stopwatch()
    {
        Timer.Interval = TimeSpan.FromMilliseconds(10);
    }

    public static void Start()
    {
        TimeStarted = DateTime.Now;
        Timer.Start();
    }

    public static void Stop() => Timer.Stop();
    
    public static void AddTick(EventHandler tick) => Timer.Tick += tick;
}