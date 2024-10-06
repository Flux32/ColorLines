using Balls.Source.Core.Struct;
using Balls.Source.Infrastructure.Services.Input;
using Reflex.Attributes;
using UnityEngine;

namespace Balls.Source.View.GameBoard.Input
{
    public class GameBoardInputRouter : MonoBehaviour
    {
        private CellPointerInput _cellInput;
        private GameBoardView _gameBoardView;
        private IGameBoardInputService _inputService;
        
        [Inject]
        private void Constructor(
            IGameBoardInputService inputService,
            GameBoardView gameBoardView, 
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
            UnityEngine.Debug.Log($"OnCursorPressed: {position}");
        }

        private void OnCursorReleased(Vector2 position)
        {
            GridPosition gridPosition = _cellInput.GetMouseCellPosition(position, _gameBoardView.Grid.CellSize);
            
            if (_gameBoardView.CanSelect(gridPosition))
                _gameBoardView.Select(gridPosition);
            
            UnityEngine.Debug.Log($"OnCursorReleased: {position}");
        }
        
        private void OnCursorMoved(Vector2 position)
        {

        }
    }
}