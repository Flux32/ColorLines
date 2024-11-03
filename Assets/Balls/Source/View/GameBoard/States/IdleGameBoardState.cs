using Balls.Core.StateMachine;
using Balls.Source.Core.StateMachine;
using Balls.Source.View.GameBoard.Commands;
using Balls.Source.View.GameBoard.Grid;

namespace Balls.Source.View.GameBoard.States
{
    public sealed class IdleGameBoardState : SimpleState
    {
        private readonly BoardView _gameBoardView;

        public IdleGameBoardState(BoardView gameBoardView)
        {
            _gameBoardView = gameBoardView;
        }

        public override void Trigger(IFsmCommand command)
        {
            if (command is InputCommand inputCommand)
                Input(inputCommand.GameBoardInputAction, inputCommand.Cell);
            else if (command is RestartGameCommand)
                _gameBoardView.Enter<RestartBoardState>();
        }

        private void Input(BoardInputAction action, CellView cell)
        {
            switch (action)
            {
                case BoardInputAction.None:
                    cell.TransitFromHoldToIdleState();
                    break;
                case BoardInputAction.Hold:
                {
                    cell.TransitToHoldState();
                    break;
                }
                case BoardInputAction.Press:
                    break;
                case BoardInputAction.CancelPress:
                    break;
                case BoardInputAction.Performed:
                {
                    if (cell.HasBall())
                    {
                        cell.SelectCell();
                        _gameBoardView.Enter<ChoiceTargetPositionState, CellView>(cell);
                    }
                    break;
                }
            }
        }
    }
}
