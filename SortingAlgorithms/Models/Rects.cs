using System.Collections.ObjectModel;
using Avalonia.Media;

namespace SortingAlgorithms.Models;

public static class Rects
{
    public static ObservableCollection<Rectangle> Rectangles { get; } = new();
}
public record Rectangle (int Height, IBrush Color);