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
        [SerializeField] private BallViewSettings _redBall;
        [SerializeField] private BallViewSettings _greenBall;
        [SerializeField] private BallViewSettings _blueBall;
        [SerializeField] private BallViewSettings _yellowBall;
        [SerializeField] private BallViewSettings _purpleBall;
        [SerializeField] private BallView _ballPrefab;

        private ObjectPool<BallView> _ballViewPool;

        private Container _container;
        
        [Serializable]
        private struct BallViewSettings
        {
            public Sprite Sprite;
            public Color AccentColor;
        }
        
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

            BallViewSettings ballSettings = ballID switch
            {
                BallId.Red => _redBall,
                BallId.Green => _greenBall,
                BallId.Blue => _blueBall,
                BallId.Yellow => _yellowBall,
                BallId.Purple => _purpleBall,
                _ => throw new InvalidOperationException($"The ball cannot be created. The sprite with ID: {ballID} is missing"),
            };

            ball.Initialize(ballSettings.Sprite, ballSettings.AccentColor);
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