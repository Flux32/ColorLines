using Balls.Source.Logic.GameBoard.Balls;

public interface IBallViewFactory
{
    BallView CreateBall(BallId ballID);
}