using System.Threading;
using Balls.Source.Core.Struct;
using Balls.Source.View.GameBoard.Balls;
using Balls.Source.View.GameBoard.Grid;
using Cysharp.Threading.Tasks;

namespace Balls.Source.View.GameBoard.Jobs
{
    public sealed class FailedPathJob : IViewJob
    {
        private readonly GridPosition _startPosition;
        private readonly GridView _gridView;

        public FailedPathJob(GridPosition startPosition, GridView gridView)
        {
            _startPosition = startPosition;
            _gridView = gridView;
        }

        public UniTask Execute(CancellationToken cancellationToken = default)
        {
            BallView ballView = _gridView[_startPosition].Ball;
            ballView.SetUnselectedState();
            return UniTask.CompletedTask;
        }
    }
}