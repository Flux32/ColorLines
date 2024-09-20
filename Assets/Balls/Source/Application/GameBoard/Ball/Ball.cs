using Balls.Core;

public class Ball
{
    public Ball(BallId id)
    {
        Id = id;
        Position = GridPosition.Zero();
    }

    public Ball(BallId id, GridPosition gridPosition)
    {
        Id = id;
        Position = gridPosition;
    }

    public BallId Id { get; private set; }
    public GridPosition Position { get; private set; }

    public Ball WithPosition(GridPosition position)
    {
        return new Ball(Id, position);
    }
}