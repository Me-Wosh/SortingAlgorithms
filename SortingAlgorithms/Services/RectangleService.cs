using System;
using System.Threading.Tasks;
using Avalonia.Media;
using SortingAlgorithms.Models;
using static SortingAlgorithms.Models.Rects;

namespace SortingAlgorithms.Services;

public sealed class RectangleService
{
    private const int NumberOfRectangles = 130;
    private readonly Random _random = new();
    public void GenerateRectangles()
    {
        const short minHeight = 5;
        const short maxHeight = 585;
        
        const byte leftRed = 20;
        const byte rightRed = 130; 
        
        for (var i = 0; i < NumberOfRectangles; i++)
        {
            var red = (byte)(leftRed - (double)(leftRed - rightRed) / (double)NumberOfRectangles * i);
            
            var color = new SolidColorBrush(Color.FromRgb(red, 35, 190));
            
            Rectangles.Add(new Rectangle(5 + (int)((double)(maxHeight - minHeight) / NumberOfRectangles * i), color));
        }
        
        ShuffleRectangles(false);
    }
    
    public async void ShuffleRectangles(bool addDelay)
    {
        for (var i = 0; i < NumberOfRectangles; i++)
        {
            var randomValue = _random.Next(0, NumberOfRectangles - 1);

            (Rectangles[i], Rectangles[randomValue]) = (Rectangles[randomValue], Rectangles[i]);
            
            await Task.Delay(TimeSpan.FromMilliseconds(addDelay ? 35 : 0));
        }
    }
}