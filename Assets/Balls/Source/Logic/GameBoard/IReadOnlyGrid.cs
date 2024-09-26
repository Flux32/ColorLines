using System.Collections.Generic;
using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard.Balls;

namespace Balls.Source.Logic.GameBoard
{
    public interface IReadOnlyGrid
    {
        Ball this[GridPosition position] { get; }
        GridSize Size { get; }
        bool IsFilled();
        IEnumerable<GridPosition> GetEmptyCells();
        bool IsBallExist(GridPosition position);
        bool IsCellExist(GridPosition position);
        bool CanPlaceBall(GridPosition position);
        bool IsEmpty();
    }
}