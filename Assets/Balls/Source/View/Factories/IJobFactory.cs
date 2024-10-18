using Balls.Source.Logic.GameBoard.Operations;
using Balls.Source.View.GameBoard.Grid;
using Balls.Source.View.GameBoard.Jobs;

namespace Balls.Source.View.Factories
{
    public interface IJobFactory
    {
        IViewJob[] CreateRestartGameJobs(GenerationOperationResult generationResult, GridView gridView);
        IViewJob[] CreateInitFirstGameJobs(GenerationOperationResult generationResult, GridView gridView);
        IViewJob[] CreateSolveJobs(MoveOperationResult moveResult, GridView gridView);
    }
}