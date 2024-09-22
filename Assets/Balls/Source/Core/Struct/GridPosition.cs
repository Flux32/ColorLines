namespace Balls.Source.Core.Struct
{
    public sealed class GridPosition
    {
        public readonly int X;
        public readonly int Y;
        
        public GridPosition(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public static GridPosition Zero()
        {
            return new GridPosition(0, 0);
        }

        public static GridPosition Left() => new GridPosition(-1, 0);
        public static GridPosition Right() => new GridPosition(1, 0);
        public static GridPosition Up() => new GridPosition(0, 1);
        public static GridPosition Down() => new GridPosition(0, -1);
        
        public override int GetHashCode()
        {
            return (X << 2) ^ Y;
        }

        public static bool operator ==(GridPosition firstPosition, GridPosition secondPosition)
        {
            return Equals(firstPosition, secondPosition);
        }

        public static bool operator !=(GridPosition firstPosition, GridPosition secondPosition)
        {
            return !(firstPosition == secondPosition);
        }

        public static GridPosition operator +(GridPosition firstPosition, GridPosition secondPosition)
        {
            return new GridPosition(firstPosition.X + secondPosition.X, firstPosition.Y + secondPosition.Y);
        }

        public static GridPosition operator -(GridPosition firstPosition, GridPosition secondPosition)
        {
            return new GridPosition(firstPosition.X - secondPosition.X, firstPosition.Y - secondPosition.Y);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            GridPosition position = (GridPosition)obj;

            return (X == position.X) && (Y == position.Y);
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}