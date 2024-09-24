using Balls.Source.Logic.GameBoard.Balls;
using Balls.Source.View.GameBoard.Balls;
using UnityEngine;

public interface IBallViewFactory
{
    BallView CreateBall(BallId ballID, Vector3 position);
    BallView CreateUnspawnedBall(BallId ballID, Vector3 position);
    void ReclaimBall(BallView ballView);
}