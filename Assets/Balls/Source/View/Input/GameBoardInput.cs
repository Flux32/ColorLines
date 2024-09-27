using Balls.Source.Core.Struct;
using Balls.Source.View.GameBoard;
using Reflex.Attributes;
using UnityEngine;

public class GameBoardInput : MonoBehaviour
{
    private CellPointerInput _cellInput;
    private GameBoardView _gameBoardView;

    [Inject]
    private void Constructor(GameBoardView gameBoardView, CellPointerInput cellInput)
    {
        _gameBoardView = gameBoardView;
        _cellInput = cellInput;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GridPosition gridPosition = _cellInput.GetMouseCellPosition(_gameBoardView.Grid.CellSize);
            if (_gameBoardView.CanSelect(gridPosition))
                _gameBoardView.Select(gridPosition);
        }
    }
}