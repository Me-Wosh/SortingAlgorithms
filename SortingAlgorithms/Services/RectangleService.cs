using System;
using System.Threading.Tasks;
using SortingAlgorithms.Models;
using static SortingAlgorithms.Models.Rects;

namespace SortingAlgorithms.Services;

public class RectangleService
{
    private const int NumberOfRectangles = 130;
    private readonly Random _random = new();
    public void GenerateRectangles()
    {
        for (var i = 0; i < NumberOfRectangles; i++)
        {
            var randomValue = _random.Next(10, 590);
            
            Rectangles.Add(new Rectangle(Height: randomValue));
        }
    }
    
    public async void ShuffleRectangles()
    {
        for (var i = 0; i < NumberOfRectangles; i++)
        {
            var randomValue = _random.Next(0, NumberOfRectangles - 1);

            (Rectangles[i], Rectangles[randomValue]) = (Rectangles[randomValue], Rectangles[i]);
            
            await Task.Delay(TimeSpan.FromMilliseconds(35));
        }
    }
}