using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Balls.Source.Logic.GameBoard.Pathfinding;
using Balls.Source.Core.Struct;
using Balls.Source.View.GameBoard.Balls;
using Balls.Source.View.GameBoard.Grid;

namespace Balls.Source.View.GameBoard.Jobs
{
    public sealed class MoveBallJob : IViewJob
    {
        private readonly Path _path;
        private readonly GridView _gridView;

        public MoveBallJob(Path path, GridView gridView)
        {
            _path = path;
            _gridView = gridView;
        }

        public UniTask Execute(CancellationToken cancellationToken)
        {
            GridPosition endPosition = _path.Points.Last();
            GridPosition startPosition = _path.Points.First();

            BallView ballView = _gridView[startPosition].Ball;
            _gridView[startPosition].DetachBall();
            _gridView[endPosition].AttachBall(ballView);
            ballView.CellPosition = _path.Points.Last();
            Vector3[] path = _path.Points.Select(position => _gridView.GridToWorldPosition(position)).ToArray();

            return ballView.Move(path, cancellationToken);
        }
    }
}