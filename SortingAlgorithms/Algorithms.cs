using static SortingAlgorithms.Models.Rects;
using System;
using System.Threading.Tasks;

namespace SortingAlgorithms;

public static class Algorithms
{
    public static async void BubbleSort(double delay)
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
                
                await Task.Delay(TimeSpan.FromMilliseconds(delay));
            }
        }
    }

    public static async void SelectionSort(double delay)
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
}