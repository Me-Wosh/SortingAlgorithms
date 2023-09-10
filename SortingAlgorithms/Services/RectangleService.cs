using System;
using System.Threading.Tasks;
using Avalonia.Media;
using SortingAlgorithms.Models;
using static SortingAlgorithms.Models.Rects;

namespace SortingAlgorithms.Services;

public sealed class RectangleService
{
    private const int NumberOfRectangles = 280;
    private readonly Random _random = new();
    
    public void GenerateRectangles()
    {
        const short minHeight = 5;
        const short maxHeight = 585;
        
        const byte leftRed = 40;
        const byte rightRed = 150; 
        
        for (var i = 1; i <= NumberOfRectangles; i++)
        {
            var red = (byte)(leftRed - (double)(leftRed - rightRed) / (double)NumberOfRectangles * i);
            
            var color = new SolidColorBrush(Color.FromRgb(red, 35, 190));
            
            Rectangles.Add(new Rectangle(
                Height: (double)(maxHeight - minHeight) / (double)NumberOfRectangles * i,
                Color: color));
        }
        
        ShuffleRectangles();
    }
    
    public async Task ShuffleRectangles(bool addDelay = false)
    {
        int i = 0, j = NumberOfRectangles - 1;
        
        while (i < j)
        {
            var randomValueLeft = _random.Next(0, j);
            var randomValueRight = _random.Next(0, j);

            (Rectangles[i], Rectangles[randomValueLeft]) = (Rectangles[randomValueLeft], Rectangles[i]);
            (Rectangles[j], Rectangles[randomValueRight]) = (Rectangles[randomValueRight], Rectangles[j]);

            i++;
            j--;
            
            await Task.Delay(TimeSpan.FromMilliseconds(addDelay ? 35 : 0));
        }
    }
}