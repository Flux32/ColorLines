using Balls.Core;

public class GameBoardGrid
{
    private Ball[,] _balls = new Ball[5, 5];

    public Ball this[GridPosition position]
    { 
        get 
        { 
            return _balls[position.X, position.Y];
        }
        private set
        {
            _balls[position.X, position.Y] = value;
        }
    }

    public int SizeX => _balls.GetLength(0);
    public int SizeY => _balls.GetLength(1);

    public bool IsBallExist(GridPosition position)
    {
        return IsCellExist(position) && this[position] != null;
    }

    public bool IsCellExist(GridPosition position)
    {
        return position.X >= 0 
            && position.Y >= 0 
            && position.X < _balls.GetLength(0) 
            && position.Y < _balls.GetLength(1);
    }

    public bool TryPlaceBall(GridPosition position, BallId ballId, out Ball placedBall)
    {
        placedBall = null;

        if (CanPlaceBall(position) == false)
            return false;

        placedBall = new Ball(ballId, position);
        this[position] = placedBall;
        return true;
    }

    public bool TryRemoveBall(GridPosition position)
    {
        this[position] = null;
        return true;
    }

    public bool CanPlaceBall(GridPosition position)
    {
        return IsBallExist(position) == false;
    }
}