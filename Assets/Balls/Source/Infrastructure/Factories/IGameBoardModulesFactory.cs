using Balls.Source.Logic.GameBoard.Generators;
using Balls.Source.Logic.GameBoard.Pathfinding;
using Balls.Source.Logic.GameBoard.Solvers;

namespace Balls.Source.Infrastructure.Factories
{
    public interface IGameBoardModulesFactory
    {
        IBallGenerator CreateBallGenerator();
        ISolver CreateSolver();
        IPathfinder CreatePathfinder();
    }
}