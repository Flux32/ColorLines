using System;
using Balls.Source.Logic.GameBoard.Balls;
using UnityEngine;

public class BallViewFactory : MonoBehaviour, IBallViewFactory
{
    [SerializeField] private Sprite _purpleBall;
    [SerializeField] private Sprite _greenBall;
    [SerializeField] private Sprite _blueBall;

    [SerializeField] private BallView _ballPrefab;

    public BallView CreateBall(BallId ballID)
    {
        BallView ballPrefab = Instantiate(_ballPrefab);

        Sprite ballSprite = ballID switch //TODO: remove
        {
            BallId.Purple => _purpleBall,
            BallId.Green => _greenBall,
            BallId.Blue => _blueBall,
            _ => throw new InvalidOperationException($"The ball cannot be created. The sprite with ID: {ballID} is missing"),
        };

        ballPrefab.Initialize(ballSprite);

        return ballPrefab;
    }
}