using Balls.Core;

public class Path
{
    public Path(GridPosition[] points, bool failed)
    {
        Points = points;
        Failed = failed;
    }

    public bool Failed { get; private set; }
    public int Size => Points.Length - 1;
    public GridPosition[] Points { get; private set; }

    public GridPosition[] Directions
    {
        get
        {
            GridPosition[] directions = new GridPosition[Points.Length - 1];

            directions[0] = GridPosition.Zero();

            for (int i = 1; i < Points.Length; i++)
                directions[i - 1] = Points[i] - Points[i - 1];

            return directions;
        }
    }
}
