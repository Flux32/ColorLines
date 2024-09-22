using Balls.Core;
using Balls.Source.Core.Struct;

namespace Balls.Source.Logic.GameBoard.Pathfinding
{
    public interface IPathfinder
    {
        Path FindPath(GridPosition startPosition, GridPosition endPosition, Grid grid);
    }
}