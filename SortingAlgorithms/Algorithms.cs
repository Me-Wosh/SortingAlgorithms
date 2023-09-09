using static SortingAlgorithms.Models.Rects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SortingAlgorithms.Models;

namespace SortingAlgorithms;

public static class Algorithms
{
    public static Dictionary<int, Func<double, Task>> SelectedAlgorithm { get; } = new()
    {
        { 0, BubbleSort },
        { 1, CocktailShakerSort },
        { 2, SelectionSort },
        { 3, InsertionSort },
        { 4, MergeSort },
        { 5, QuickSort }
    };
    private static async Task BubbleSort(double delay)
    {
        Stopwatch.Start();
        
        var active = true;
        var end = Rectangles.Count - 1;

        while (active)
        {
            active = false;

            for (var i = 0; i < end; i++)
            {
                if (Rectangles[i + 1].Height >= Rectangles[i].Height)
                    continue;
                
                (Rectangles[i + 1], Rectangles[i]) = (Rectangles[i], Rectangles[i + 1]);
                active = true;
                
                await Task.Delay(TimeSpan.FromMilliseconds(delay));
            }

            end--;
        }
        
        Stopwatch.Stop();
    }
    
    private static async Task CocktailShakerSort(double delay)
    {
        Stopwatch.Start();

        var active = true;
        var start = 0;
        var end = Rectangles.Count - 1;

        while (active)
        {
            active = false;

            for (var i = start; i < end; i++)
            {
                if (Rectangles[i].Height <= Rectangles[i + 1].Height)
                    continue;
                
                (Rectangles[i], Rectangles[i + 1]) = (Rectangles[i + 1], Rectangles[i]);
                active = true;
                
                await Task.Delay(TimeSpan.FromMilliseconds(delay));
            }

            end--;

            for (var i = end; i > 0; i--)
            {
                if (Rectangles[i].Height >= Rectangles[i - 1].Height)
                    continue;
                
                (Rectangles[i], Rectangles[i - 1]) = (Rectangles[i - 1], Rectangles[i]);
                active = true;
                
                await Task.Delay(TimeSpan.FromMilliseconds(delay));
            }

            start++;
        }
        
        Stopwatch.Stop();
    }

    private static async Task SelectionSort(double delay)
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

            await Task.Delay(TimeSpan.FromMilliseconds(delay + 30));
        }
        
        Stopwatch.Stop();
    }

    private static async Task InsertionSort(double delay)
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

    private static async Task MergeSort(double delay)
    {
        Stopwatch.Start();

        await MergeSort(0, Rectangles.Count - 1, delay);
        
        Stopwatch.Stop();
    }

    private static async Task MergeSort(int left, int right, double delay)
    {
        if (left >= right)
            return;

        var middle = (right - left) / 2 + left;

        await MergeSort(left, middle, delay);
        await MergeSort(middle + 1, right, delay);
        await Merge(left, right, middle, delay);
    }

    private static async Task Merge(int left, int right, int middle, double delay)
    {
        var leftArray = new Rectangle[middle - left + 1];
        var rightArray = new Rectangle[right - middle];

        for (var i = 0; i < leftArray.Length; i++)
            leftArray[i] = Rectangles[i + left];
        
        for (var i = 0; i < rightArray.Length; i++)
            rightArray[i] = Rectangles[middle + 1 + i];

        int j = left, l = 0, r = 0;
        
        while (l < leftArray.Length && r < rightArray.Length)
        {
            if (leftArray[l].Height < rightArray[r].Height)
            {
                Rectangles[j] = leftArray[l];
                l++;
            }

            else
            {
                Rectangles[j] = rightArray[r];
                r++;
            }

            j++;
            await Task.Delay(TimeSpan.FromMilliseconds(delay));
        }

        while (l < leftArray.Length)
        {
            Rectangles[j] = leftArray[l];
            j++;
            l++;
            await Task.Delay(TimeSpan.FromMilliseconds(delay));
        }
        
        while (r < rightArray.Length)
        {
            Rectangles[j] = rightArray[r]; 
            j++;
            r++;
            await Task.Delay(TimeSpan.FromMilliseconds(delay));
        }
    }

    private static async Task QuickSort(double delay)
    {
        Stopwatch.Start();

        await QuickSort(0, Rectangles.Count - 1, delay);
            
        Stopwatch.Stop();
    }

    private static async Task QuickSort(int first, int last, double delay)
    {
        if (first > last)
            return;
        
        var pivotIndex = await Partition(first, last, delay);
        
        await QuickSort(first, pivotIndex - 1, delay);
        await QuickSort(pivotIndex + 1, last, delay);
    }

    private static async Task<int> Partition(int first, int last, double delay)
    {
        var middle = (first + last) / 2;
        
        if (Rectangles[middle].Height < Rectangles[first].Height)
            (Rectangles[first], Rectangles[middle]) = (Rectangles[middle], Rectangles[first]);
        
        if (Rectangles[last].Height < Rectangles[first].Height)
            (Rectangles[first], Rectangles[last]) = (Rectangles[last], Rectangles[first]);

        if (Rectangles[middle].Height < Rectangles[last].Height)
            (Rectangles[middle], Rectangles[last]) = (Rectangles[last], Rectangles[middle]);

        var pivot = Rectangles[last].Height;
            
        var i = first;

        for (var j = first; j < last; j++)
        {
            if (Rectangles[j].Height > pivot)
                continue;
            
            (Rectangles[i], Rectangles[j]) = (Rectangles[j], Rectangles[i]);
            i++;
            
            await Task.Delay(TimeSpan.FromMilliseconds(delay));
        }

        (Rectangles[i], Rectangles[last]) = (Rectangles[last], Rectangles[i]);

        return i;
    }
}