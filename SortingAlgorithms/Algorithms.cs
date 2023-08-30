using static SortingAlgorithms.Models.Rects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SortingAlgorithms;

public static class Algorithms
{
    public static Dictionary<int, Action<double>> SelectedAlgorithm { get; } = new()
    {
        { 0, BubbleSort },
        { 1, SelectionSort },
        { 2, InsertionSort }
    };
    private static async void BubbleSort(double delay)
    {
        Stopwatch.Start();
        
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
                
                await Task.Delay(TimeSpan.FromMilliseconds(delay));
            }
        }
        
        Stopwatch.Stop();
    }

    private static async void SelectionSort(double delay)
    {
        Stopwatch.Start();
        
        for (var i = 0; i < Rectangles.Count; i++)
        {
            var min = i;

            for (var j = i; j < Rectangles.Count; j++)
            {
                if (Rectangles[j].Height >= Rectangles[min].Height)
                    continue;

                min = j;
            }

            (Rectangles[i], Rectangles[min]) = (Rectangles[min], Rectangles[i]);

            await Task.Delay(TimeSpan.FromMilliseconds(delay));
        }
        
        Stopwatch.Stop();
    }

    private static async void InsertionSort(double delay)
    {
        Stopwatch.Start();

        for (var i = 1; i < Rectangles.Count; i++)
        {
            for (var j = i; j > 0; j--)
            {
                if (Rectangles[j].Height >= Rectangles[j - 1].Height)
                    continue;

                (Rectangles[j], Rectangles[j - 1]) = (Rectangles[j - 1], Rectangles[j]);

                await Task.Delay(TimeSpan.FromMilliseconds(delay));
            }
        }
        
        Stopwatch.Stop();
    }
}