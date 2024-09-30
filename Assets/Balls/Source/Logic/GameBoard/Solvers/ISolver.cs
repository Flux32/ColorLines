using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard.Operations;

namespace Balls.Source.Logic.GameBoard.Solvers
{
    public interface ISolver
    {
        SolveResult Solve(GridPosition position, Grid grid);
    }
}