using Balls.Source.Logic.GameBoard;
using Balls.Source.Logic.GameBoard.Operations;
using Balls.Source.View.GameBoard.Grid;
using Balls.Source.View.GameBoard.Jobs;

namespace Balls.Source.View.Factories
{
    public sealed class JobFactory : IJobFactory
    {
        private readonly IBallViewFactory _ballViewFactory;
        private readonly IEffectsFactory _effectsFactory;
        
        public JobFactory(IBallViewFactory ballViewFactory, IEffectsFactory effectsFactory)
        {
            _ballViewFactory = ballViewFactory;
            _effectsFactory = effectsFactory;
        }

        public IViewJob[] CreateRestartGameJobs(GenerationOperationResult generationResult, GridView gridView)
        {
            return new IViewJob[]
            {
                new ClearGridJob(gridView, _ballViewFactory),
                new SpawnBallJob(_ballViewFactory, gridView, generationResult.SpawnedBalls)
            };
        }

        public IViewJob[] CreateInitFirstGameJobs(GenerationOperationResult generationResult, GridView gridView)
        {
            return new IViewJob[]
            {
                new SpawnBallJob(_ballViewFactory, gridView, generationResult.SpawnedBalls)
            };
        }

        public IViewJob[] CreateSolveJobs(MoveOperationResult moveResult, GridView gridView)
        {
            if (moveResult.Result == MoveResult.PathFailed)
                return new IViewJob[]
                {
                    new FailedPathJob(moveResult.MovedResult.Ball.Position, gridView),
                };
                
            IViewJob[] solveBallJobsAfterGenerate =
                new IViewJob[moveResult.SolvedBallsAfterGeneration.Count];

            for (int i = 0; i < solveBallJobsAfterGenerate.Length; i++)
                solveBallJobsAfterGenerate[i] = 
                    new SolveBallJob(moveResult.SolvedBallsAfterGeneration[i], gridView, _ballViewFactory, _effectsFactory);
            
            return new IViewJob[] {
                new MoveBallJob(moveResult.MovedResult.Path, gridView),
                new SolveBallJob(moveResult.SolvedBallsAfterMove, gridView, _ballViewFactory, _effectsFactory),
                new SpawnBallJob(_ballViewFactory, gridView, moveResult.GenerationOperationResult.SpawnedBalls),
                new WhenAllJobsCompletedJob(solveBallJobsAfterGenerate),
            };
        }
    }
}
