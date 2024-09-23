using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Balls.Source.Infrastructure.Extensions;
using Balls.Source.Logic.GameBoard.Balls;

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

        public UniTask Execute(CancellationToken cancellationToken)
        {
            foreach (Ball ball in _balls)
            {
                Vector3 ballPosition = ball.Position.ToVector3();
                BallView ballView = _ballFactory.CreateBall(ball.Id, ballPosition);
                ballView.CellPosition = ball.Position;
                _ballsViewGrid[ball.Position.X, ball.Position.Y] = ballView;
            }
        
            return UniTask.CompletedTask;
        }
    }
}