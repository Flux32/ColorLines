using Balls.Source.Logic.GameBoard.Operations;
using Balls.Source.View.GameBoard;
using Balls.Source.View.GameBoard.Jobs;
using UnityEngine;

namespace Balls.Source.View.Factories
{
    public sealed class JobFactory : IJobFactory
    {
        private readonly IBallViewFactory _ballViewFactory;

        public JobFactory(IBallViewFactory ballViewFactory)
        {
            _ballViewFactory = ballViewFactory;
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
            IViewJob[] solveBallJobsAfterGenerate =
                new IViewJob[moveResult.SolvedBallsAfterGeneration.Count];

            for (int i = 0; i < solveBallJobsAfterGenerate.Length; i++)
                solveBallJobsAfterGenerate[i] = 
                    new SolveBallJob(moveResult.SolvedBallsAfterGeneration[i], gridView, _ballViewFactory);
            
            return new IViewJob[] {
                new MoveBallJob(moveResult.MovedResult.Path, gridView),
                new SolveBallJob(moveResult.SolvedBallsAfterMove, gridView, _ballViewFactory),
                new SpawnBallJob(_ballViewFactory, gridView, moveResult.GenerationOperationResult.SpawnedBalls),
                new WhenAllJobsCompletedJob(solveBallJobsAfterGenerate),
            };
        }
    }
}
