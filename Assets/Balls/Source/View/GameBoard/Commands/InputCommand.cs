using Balls.Source.Core.StateMachine;
using Balls.Source.Core.Struct;
using Balls.Source.View.GameBoard.Grid;

namespace Balls.Source.View.GameBoard.Commands
{
    public sealed class InputCommand : IFsmCommand
    {
        public InputCommand(BoardInputAction gameBoardInputAction, CellView cell, GridPosition gridPosition)
        {
            GameBoardInputAction = gameBoardInputAction;
            Cell = cell;
            CellPosition = gridPosition;
        }

        public BoardInputAction GameBoardInputAction { get; }
        public CellView Cell { get; }
        public GridPosition CellPosition { get; }
    }
}