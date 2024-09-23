using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Balls.Source.Logic.GameBoard.Pathfinding;
using Balls.Source.Infrastructure.Extensions;
using Balls.Source.Core.Struct;
using Balls.Source.View.GameBoard.Balls;

namespace Balls.Source.View.GameBoard.Jobs
{
    public sealed class MoveBallJob : IViewJob
    {
        private readonly Path _path;
        private readonly BallView[,] _grid;

        public MoveBallJob(Path path, BallView[,] grid)
        {
            _path = path;
            _grid = grid;
        }

        public UniTask Execute(CancellationToken cancellationToken)
        {
            GridPosition endPosition = _path.Points.Last();
            GridPosition startPosition = _path.Points.First();

            BallView ballView = _grid[startPosition.X, startPosition.Y];
            _grid[startPosition.X, startPosition.Y] = null;
            _grid[endPosition.X, endPosition.Y] = ballView;
            ballView.CellPosition = _path.Points.Last();
            Vector3[] path = _path.Points.Select(position => position.ToVector3()).ToArray();

            return ballView.Move(path, cancellationToken);
        }
    }
}