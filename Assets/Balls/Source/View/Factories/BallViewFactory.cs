using System;
using Balls.Source.Logic.GameBoard.Balls;
using Balls.Source.View.GameBoard.Balls;
using Reflex.Attributes;
using Reflex.Core;
using Reflex.Injectors;
using UnityEngine;
using UnityEngine.Pool;

namespace Balls.Source.View.Factories
{
    public sealed class BallViewFactory : MonoBehaviour, IBallViewFactory
    {
        [SerializeField] private Sprite _redBall;
        [SerializeField] private Sprite _greenBall;
        [SerializeField] private Sprite _blueBall;
        [SerializeField] private Sprite _yellowBall;
        [SerializeField] private Sprite _purpleBall;
        [SerializeField] private BallView _ballPrefab;

        private ObjectPool<BallView> _ballViewPool;

        private Container _container;
        
        private void Awake()
        {
            _ballViewPool = new ObjectPool<BallView>(CreateBall);
        }

        [Inject]
        private void Constructor(Container container)
        {
            _container = container;
        }
        
        public BallView CreateBall(BallId ballID, Vector3 position)
        {
            BallView ball = _ballViewPool.Get();

            Sprite ballSprite = ballID switch //TODO: remove
            {
                BallId.Red => _redBall,
                BallId.Green => _greenBall,
                BallId.Blue => _blueBall,
                BallId.Yellow => _yellowBall,
                BallId.Purple => _purpleBall,
                _ => throw new InvalidOperationException($"The ball cannot be created. The sprite with ID: {ballID} is missing"),
            };

            ball.SetBallSprite(ballSprite);
            ball.transform.position = position;
            ball.gameObject.SetActive(true);
        
            return ball;
        }

        public BallView CreateUnspawnedBall(BallId ballID, Vector3 position)
        {
            BallView ballView = CreateBall(ballID, position);
            ballView.SetUnspawnedState();
            return ballView;
        }

        public void ReclaimBall(BallView ballView)
        {
            ballView.gameObject.SetActive(false);
            _ballViewPool.Release(ballView);
        }

        private BallView CreateBall()
        {
            BallView ballView = Instantiate(_ballPrefab);
            GameObjectInjector.InjectObject(ballView.gameObject, _container);
            return ballView;
        }
            
    }
}