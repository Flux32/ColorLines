using System.Threading;
using Balls.Source.Core.Struct;
using Cysharp.Threading.Tasks;

namespace Balls.Source.View.GameBoard.Jobs
{
    public sealed class ClearGridJob : IViewJob
    {
        private readonly GridView _grid;
        private readonly IBallViewFactory _ballViewFactory;

        public ClearGridJob(GridView grid, IBallViewFactory ballViewFactory)
        {
            _grid = grid;
            _ballViewFactory = ballViewFactory;
        }

        public UniTask Execute(CancellationToken cancellationToken = default)
        {
            for (int x = 0; x < _grid.Size.Width; x++)
            {
                for (int y = 0; y < _grid.Size.Height; y++)
                {
                    GridPosition gridPosition = new GridPosition(x, y);

                    if (_grid.IsBallExist(gridPosition) == false)
                        continue;
                    
                    _ballViewFactory.ReclaimBall(_grid[gridPosition].Ball);
                    _grid[gridPosition].DetachBall();
                }
            }
            return UniTask.CompletedTask;
        }
    }
}