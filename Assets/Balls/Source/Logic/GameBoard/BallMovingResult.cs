using Balls.Source.Logic.GameBoard.Balls;
using Balls.Source.Logic.GameBoard.Pathfinding;

namespace Balls.Source.Logic.GameBoard
{
    public sealed class BallMovingResult
    {
        public BallMovingResult(Path path, Ball ball)
        {
            Path = path;
            Ball = ball;
        }

        public Path Path { get; private set; }
        public Ball Ball { get; private set; }
    }
}