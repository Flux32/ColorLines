using Reflex.Attributes;
using UnityEngine;

public class CellMouseDebugger : MonoBehaviour
{
    private GameBoardGrid _gameBoardGrid;
    private CellPointerInput _cellInput;

    [Inject]
    private void Constructor(GameBoardGrid gameBoardGrid, CellPointerInput cellInput)
    {
        _gameBoardGrid = gameBoardGrid;
        _cellInput = cellInput;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2Int cellPosition = _cellInput.GetMouseCellPosition();

            Debug.Log($"IsCellExist: {_gameBoardGrid.IsCellExist(cellPosition.ToGridPosition())}");
            Debug.Log($"IsBallExist: {_gameBoardGrid.IsBallExist(cellPosition.ToGridPosition())}");
        }
    }
}
