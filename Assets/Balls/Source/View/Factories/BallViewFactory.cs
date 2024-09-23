using System;
using Balls.Source.Logic.GameBoard.Balls;
using Balls.Source.View.GameBoard.Balls;
using UnityEngine;

namespace Balls.Source.View.Factories
{
    public class BallViewFactory : MonoBehaviour, IBallViewFactory
    {
        [SerializeField] private Sprite _purpleBall;
        [SerializeField] private Sprite _greenBall;
        [SerializeField] private Sprite _blueBall;
        [SerializeField] private BallView _ballPrefab;

        public BallView CreateBall(BallId ballID, Vector3 position)
        {
            BallView ball = Instantiate(_ballPrefab);

            Sprite ballSprite = ballID switch //TODO: remove
            {
                BallId.Purple => _purpleBall,
                BallId.Green => _greenBall,
                BallId.Blue => _blueBall,
                _ => throw new InvalidOperationException($"The ball cannot be created. The sprite with ID: {ballID} is missing"),
            };

            ball.Initialize(ballSprite);
            ball.transform.position = position;
        
            return ball;
        }

        public BallView CreateUnspawnedBall(BallId ballID, Vector3 position)
        {
            BallView ballView = CreateBall(ballID, position);
            ballView.SetUnspawnedState();
            return ballView;
        }
    }
}