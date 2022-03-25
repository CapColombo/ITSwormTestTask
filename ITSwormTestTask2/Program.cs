class Point : IComparable<Point>
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public double Distance { get; private set; } // расстояние от начала координат до точки

    public Point(int x, int y)
    {
        X = x;
        Y = y;
        Distance = Math.Sqrt(X * X + Y * Y);
    }

    public int CompareTo(Point? other)
    {
        if (other?.Distance < Distance || other == null)
            return 1;
        else if (other?.Distance > Distance)
            return -1;
        else
            return 0;
    }
}

class Program
{
    static void Main()
    {
        // по часовой
        List<Point> clockwise = new() 
        {
            new Point(10, 1),
            new Point(5, 3),
            new Point(1, 8),
            new Point(3, 16),
            new Point(9, 19),
            new Point(16, 18),
            new Point(20, 10),
            new Point(18, 4),
        };

        // инициализируем новый список старым, чтобы создался новый объект в памяти
        List<Point> counterClockwise = new(clockwise); 
        counterClockwise.Reverse(); // против часовой

        Console.WriteLine($"Обход по часовой: {IsClockwise(clockwise)}");
        Console.WriteLine($"Обход по часовой: {IsClockwise(counterClockwise)}");
    }

    public static bool IsClockwise(List<Point> points)
    {
        // находим крайние точки
        Point left = points.OrderBy(p => p.X).First();
        Point bottom = points.OrderBy(p => p.Y).First();
        Point top = points.OrderByDescending(p => p.Y).First();

        // изменяем список так, чтобы первой точка была левая
        int indexLeft = points.IndexOf(left);
        var carvedRange = points.GetRange(indexLeft, points.Count - indexLeft);
        points.RemoveRange(indexLeft, points.Count - indexLeft);
        points.InsertRange(0, carvedRange);

        // если ближайшая крайняя точка будет снизу, то обход против часовой
        foreach (var item in points)
        {
            if (item == bottom)
                return false;
            else if (item == top)
                return true;
        }
        throw new Exception();
    }
}
