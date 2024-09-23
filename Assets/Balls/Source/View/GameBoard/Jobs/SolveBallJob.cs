using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Balls.Source.Logic.GameBoard.Balls;
using Cysharp.Threading.Tasks;
using UnityEngine;

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
        
            foreach (var ballView in solvedViews)
            {
                Debug.Log("aaa");
                Debug.Log(ballView.gameObject.name, ballView.gameObject);
                Object.Destroy(ballView.gameObject);
            }

            await UniTask.WaitForSeconds(0.1f, cancellationToken: cancellationToken);
        }
    }
}