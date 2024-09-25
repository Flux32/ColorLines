using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Balls.Source.Infrastructure.Extensions;
using Balls.Source.Logic.GameBoard.Balls;
using Balls.Source.View.GameBoard.Balls;

namespace Balls.Source.View.GameBoard.Jobs
{
    public sealed class SpawnBallJob : IViewJob
    {
        private readonly BallView[,] _ballsViewGrid;
        private readonly IEnumerable<Ball> _balls;
        private readonly IBallViewFactory _ballFactory;
    
        public SpawnBallJob(IBallViewFactory ballFactory, BallView[,] ballsViewGrid, IEnumerable<Ball> balls)
        {
            _ballFactory = ballFactory;
            _ballsViewGrid = ballsViewGrid;
            _balls = balls;
        }

        public async UniTask Execute(CancellationToken cancellationToken)
        {   
            List<UniTask> tasks = new List<UniTask>();
            
            foreach (Ball ball in _balls)
            {
                await UniTask.WaitForSeconds(0.1f, cancellationToken: cancellationToken);
                Vector3 ballPosition = ball.Position.ToVector3();
                BallView ballView = _ballFactory.CreateUnspawnedBall(ball.Id, ballPosition);
                ballView.CellPosition = ball.Position;
                _ballsViewGrid[ball.Position.X, ball.Position.Y] = ballView;
                tasks.Add(ballView.PlaySpawnAnimation());
            }
            
            await UniTask.WhenAll(tasks).AttachExternalCancellation(cancellationToken);
        }
    }
}