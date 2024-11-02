using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Balls.Source.Logic.GameBoard.Operations;
using Balls.Source.View.Effects;
using Balls.Source.View.Factories;
using Balls.Source.View.GameBoard.Balls;
using Balls.Source.View.GameBoard.Grid;
using Cysharp.Threading.Tasks;

namespace Balls.Source.View.GameBoard.Jobs
{
    public sealed class SolveBallJob : IViewJob
    {
        private readonly GridView _gridView;
        private readonly SolveResult _solveResult;
        private readonly IBallViewFactory _ballViewFactory;
        private readonly IEffectsFactory _effectsFactory;
        
        public SolveBallJob(
            SolveResult solveResult, 
            GridView gridView, 
            IBallViewFactory ballViewFactory,
            IEffectsFactory effectsFactory)
        {
            _solveResult = solveResult;
            _gridView = gridView;
            _ballViewFactory = ballViewFactory;
            _effectsFactory = effectsFactory;
        }

        public async UniTask Execute(CancellationToken cancellationToken)
        {
            if (_solveResult.SolveExecuted == false)
                return;
            
            CellView[] solvedCells = _solveResult.Balls.Select(cell => _gridView[cell.Position]).ToArray();
            CellView originCell = _gridView[_solveResult.SolveOrigin.Position];
            
            int originCellIndex = Array.IndexOf(solvedCells, originCell);
            
            List<UniTask> animationTasks = new List<UniTask>();

            int step = 1;

            int maxSteps = Math.Max(originCellIndex - 1, solvedCells.Length - originCellIndex - 1);
            
            animationTasks.Add(SolveBall(solvedCells[originCellIndex]));
            
            while (step <= maxSteps)
            {
                int leftCursor = originCellIndex - step;
                int rightCursor = originCellIndex + step;
                
                if (leftCursor > 0)
                    animationTasks.Add(SolveBall(solvedCells[leftCursor]));

                if (rightCursor < solvedCells.Length)
                    animationTasks.Add(SolveBall(solvedCells[rightCursor]));

                step++;
                await UniTask.WaitForSeconds(0.1f, cancellationToken: cancellationToken);
            }
            
            await UniTask.WhenAll(animationTasks).AttachExternalCancellation(cancellationToken);
        }

        private void SortCells()
        {

        }

        private async UniTask SolveBall(CellView cell)
        {
            BallView ball = cell.Ball;
            await ball.PlaySolveAnimation();

            SimpleEffect popEffect = _effectsFactory.CreatePopEffect();
            popEffect.transform.position = cell.Ball.transform.position;
            popEffect.Play().Forget();
            await UniTask.WaitForSeconds(0.3f);
            
            FloatingValueEffect floatingValueEffect = _effectsFactory.CreateFloatingValueEffect();
            floatingValueEffect.transform.position = cell.Ball.transform.position;
            floatingValueEffect.Play(1).Forget();
        
            cell.DetachBall();                    

            _ballViewFactory.ReclaimBall(ball);
        }
    }
}