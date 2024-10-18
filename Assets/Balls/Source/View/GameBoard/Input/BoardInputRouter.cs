using Balls.Source.Core.Struct;
using Balls.Source.Infrastructure.Services.Input;
using Reflex.Attributes;
using UnityEngine;

namespace Balls.Source.View.GameBoard.Input
{
    public class BoardInputRouter : MonoBehaviour
    {
        private CellPointerInput _cellInput;
        private BoardView _gameBoardView;
        private IGameBoardInputService _inputService;

        private GridPosition _cursorGridPosition = new GridPosition(-1, -1);
        
        [Inject]
        private void Constructor(
            IGameBoardInputService inputService,
            BoardView gameBoardView, 
            CellPointerInput cellInput)
        {
            _inputService = inputService;
            _gameBoardView = gameBoardView;
            _cellInput = cellInput;
        }

        private void OnEnable()
        {
            _inputService.CursorMoved += OnCursorMoved;
            _inputService.CursorPressed += OnCursorPressed;
            _inputService.CursorReleased += OnCursorReleased;
        }
        
        private void OnDisable()
        {
            _inputService.CursorMoved -= OnCursorMoved;
            _inputService.CursorPressed -= OnCursorPressed;
            _inputService.CursorReleased -= OnCursorReleased;
        }
        
        private void OnCursorPressed(Vector2 position)
        {
            _gameBoardView.Input(BoardInputAction.Press, _cursorGridPosition);
        }

        private void OnCursorReleased(Vector2 position)
        {
            _gameBoardView.Input(BoardInputAction.Performed, _cursorGridPosition);
        }
        
        private void OnCursorMoved(Vector2 position)
        {
            GridPosition gridPosition = _cellInput.GetMouseCellPosition(position, _gameBoardView.Grid.CellSize);
            
            if (gridPosition == _cursorGridPosition)
                return;
            
            _gameBoardView.Input(BoardInputAction.None, _cursorGridPosition);
            _gameBoardView.Input(BoardInputAction.Hold, gridPosition);
            _cursorGridPosition = gridPosition; 
        }
    }
}