using Balls.Source.Logic.GameBoard.Balls;
using UnityEngine;

public interface IBallViewFactory
{
    BallView CreateBall(BallId ballID, Vector3 position);
}