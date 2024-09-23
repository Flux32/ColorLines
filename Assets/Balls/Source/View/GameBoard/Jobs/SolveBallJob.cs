using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Balls.Source.Logic.GameBoard.Balls;
using Cysharp.Threading.Tasks;

namespace Balls.Source.View.GameBoard.Jobs
{
    public sealed class SolveBallJob : IViewJob
    {
        private readonly BallView[,] _grid;
        private readonly IEnumerable<Ball> _balls;
    
        public SolveBallJob(IEnumerable<Ball> balls, BallView[,] grid)
        {
            _balls = balls;
            _grid = grid;
        }
    
        public async UniTask Execute(CancellationToken cancellationToken)
        {
            BallView[] solvedViews = _balls.Select(ball => _grid[ball.Position.X, ball.Position.Y]).ToArray();
            List<UniTask> animationTasks = new List<UniTask>();
            
            foreach (BallView ballView in solvedViews)
            {
                animationTasks.Add(ballView.PlaySolveAnimation());
                await UniTask.WaitForSeconds(0.1f, cancellationToken: cancellationToken);
                _grid[ballView.CellPosition.X, ballView.CellPosition.Y] = null;
            }
            await UniTask.WhenAll(animationTasks).AttachExternalCancellation(cancellationToken);
        }
    }
}