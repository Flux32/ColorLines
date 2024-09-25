using Balls.Source.Core.Struct;
using Reflex.Attributes;
using UnityEngine;
using Grid = Balls.Source.Logic.GameBoard.Grid;

namespace Balls.Source.View.Debug
{
    public sealed class MouseDebugger : MonoBehaviour
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
                GridPosition cellPosition = _cellInput.GetMouseCellPosition();

                UnityEngine.Debug.Log($"IsCellExist: {_grid.IsCellExist(cellPosition)}");
                UnityEngine.Debug.Log($"IsBallExist: {_grid.IsBallExist(cellPosition)}");
            }
        }
    }
}
