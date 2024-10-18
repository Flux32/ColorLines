using Balls.Source.Core.Struct;
using Balls.Core.StateMachine;
using Balls.Source.View.GameBoard;
using Balls.Source.View.GameBoard.Grid;

public sealed class InputCommand : IFSMCommand
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