﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Balls.Source.Logic.GameBoard.Balls;
using Balls.Source.View.GameBoard.Balls;
using Cysharp.Threading.Tasks;

namespace Balls.Source.View.GameBoard.Jobs
{
    public sealed class SolveBallJob : IViewJob
    {
        private readonly GridView _gridView;
        private readonly IEnumerable<Ball> _balls;
        private readonly IBallViewFactory _ballViewFactory;

        public SolveBallJob(IEnumerable<Ball> balls, GridView gridView, IBallViewFactory ballViewFactory)
        {
            _balls = balls;
            _gridView = gridView;
            _ballViewFactory = ballViewFactory;
        }

        public async UniTask Execute(CancellationToken cancellationToken)
        {
            BallView[] solvedBallsViews = _balls.Select(ball => _gridView[ball.Position]).ToArray();
            List<UniTask> animationTasks = new List<UniTask>();
            
            foreach (BallView ballView in solvedBallsViews)
            {
                animationTasks.Add(ballView.PlaySolveAnimation());
                await UniTask.WaitForSeconds(0.1f, cancellationToken: cancellationToken);
                _gridView[ballView.CellPosition] = null;
            }
            await UniTask.WhenAll(animationTasks).AttachExternalCancellation(cancellationToken);

            foreach (BallView ballView in solvedBallsViews)
                _ballViewFactory.ReclaimBall(ballView);
        }
    }
}