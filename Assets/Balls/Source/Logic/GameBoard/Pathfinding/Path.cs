using Balls.Source.Core.Struct;

namespace Balls.Source.Logic.GameBoard.Pathfinding
{
    public sealed class Path
    {
        public Path(GridPosition[] points, bool failed)
        {
            Points = points;
            Failed = failed;
        }

        public bool Failed { get; }
        public GridPosition[] Points { get; }

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
}
