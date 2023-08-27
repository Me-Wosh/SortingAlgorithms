using System.Collections.ObjectModel;

namespace SortingAlgorithms.Models;

public class Rects
{
    public static ObservableCollection<Rectangle> Rectangles { get; } = new();
}
public record Rectangle (int Height);