using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Balls.Source.Infrastructure.Extensions;
using Balls.Source.Logic.GameBoard.Balls;
using Balls.Source.View.GameBoard.Balls;

namespace Balls.Source.View.GameBoard.Jobs
{
    public sealed class SpawnBallJob : IViewJob
    {
        private readonly GridView _gridView;
        private readonly IEnumerable<Ball> _balls;
        private readonly IBallViewFactory _ballFactory;
    
        public SpawnBallJob(IBallViewFactory ballFactory, GridView gridView, IEnumerable<Ball> balls)
        {
            _ballFactory = ballFactory;
            _gridView = gridView;
            _balls = balls;
        }

        public async UniTask Execute(CancellationToken cancellationToken)
        {   
            List<UniTask> tasks = new List<UniTask>();
            
            foreach (Ball ball in _balls)
            {
                Vector3 ballPosition = _gridView.GridToWorldPosition(ball.Position);
                BallView ballView = _ballFactory.CreateUnspawnedBall(ball.Id, ballPosition);
                ballView.CellPosition = ball.Position;
                _gridView[ball.Position].AttachBall(ballView);
                tasks.Add(ballView.PlaySpawnAnimation());
            }
            
            await UniTask.WhenAll(tasks).AttachExternalCancellation(cancellationToken);
        }
    }
}