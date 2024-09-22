using Balls.Source.Logic.GameBoard;
using Reflex.Attributes;
using UnityEngine;
using Grid = Balls.Source.Logic.GameBoard.Grid;

public class CellMouseDebugger : MonoBehaviour
{
    private Grid _grid;
    private CellPointerInput _cellInput;

    [Inject]
    private void Constructor(Grid grid, CellPointerInput cellInput)
    {
        _grid = grid;
        _cellInput = cellInput;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2Int cellPosition = _cellInput.GetMouseCellPosition();

            Debug.Log($"IsCellExist: {_grid.IsCellExist(cellPosition.ToGridPosition())}");
            Debug.Log($"IsBallExist: {_grid.IsBallExist(cellPosition.ToGridPosition())}");
        }
    }
}
