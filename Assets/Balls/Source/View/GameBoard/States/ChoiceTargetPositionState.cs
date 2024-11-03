using Balls.Core.StateMachine;
using Balls.Source.Core.StateMachine;
using Balls.Source.View.GameBoard.Balls;
using Balls.Source.View.GameBoard.Commands;
using Balls.Source.View.GameBoard.Grid;

namespace Balls.Source.View.GameBoard.States
{
    public sealed class ChoiceTargetPositionState : PayloadState<CellView>
    {
        private readonly BoardView _gameBoardView;
        private readonly GridView _gridView;

        private CellView _selectedCell;

        public ChoiceTargetPositionState(GridView gridView, BoardView gameBoardView)
        {
            _gameBoardView = gameBoardView;
            _gridView = gridView;
        }

        public override void Enter(CellView selectedCell)
        {
            _selectedCell = selectedCell;
        }

        public override void Trigger(IFsmCommand command)
        {
            if (command is RestartGameCommand)
            {
                _gameBoardView.Enter<RestartBoardState>();
            }
            else if (command is InputCommand inputCommand)
            {
                CellView cellView = inputCommand.Cell;
                BallView ball = _selectedCell.Ball;

                switch (inputCommand.GameBoardInputAction)
                {
                    case BoardInputAction.None:
                    {
                        cellView.TransitFromHoldToIdleState();
                        break;
                    }
                    case BoardInputAction.Hold:
                    {
                        cellView.TransitToHoldState(ball.AccentColor);
                        break;
                    }
                    case BoardInputAction.Press:
                    {
                        cellView.TransitToPressedState();
                        break;
                    }
                    case BoardInputAction.CancelPress:
                    {
                        cellView.TransitFromHoldToIdleState();
                        break;
                    }
                    case BoardInputAction.Performed:
                    {
                        if (ball.CellPosition == inputCommand.CellPosition)
                        {
                            cellView.UnselectCell();
                            _gameBoardView.Enter<IdleGameBoardState>();
                            return;
                        }

                        if (_gridView.IsBallExist(inputCommand.CellPosition))
                        {
                            _selectedCell.UnselectCell();
                            cellView.SelectCell();
                            _selectedCell = cellView;
                            return;
                        }
                        
                        _selectedCell.TransitFromHoldToIdleState();
                        _gridView[inputCommand.CellPosition].TransitFromHoldToIdleState();
                        _gameBoardView.Enter<MakeMoveBoardState, MoveRequest>(new MoveRequest(ball.CellPosition, inputCommand.CellPosition));
                        break;
                    }
                }
            }
        }
    }
}
