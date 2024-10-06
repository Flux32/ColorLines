using Balls.Source.Core.Struct;
using Balls.Source.View.GameBoard.Input;
using Reflex.Attributes;
using UnityEngine;

namespace Balls.Source.View.Debug
{
    public sealed class MouseDebugger : MonoBehaviour
    {
        private Logic.GameBoard.GameBoard _gameBoard;
        private CellPointerInput _cellInput;

        [Inject]
        private void Constructor(Logic.GameBoard.GameBoard gameBoard, CellPointerInput cellInput)
        {
            _gameBoard = gameBoard;
            _cellInput = cellInput;
        }

        private void Update()
        {
            //if (Input.GetMouseButtonDown(1))
           // {
             //   GridPosition cellPosition = _cellInput.GetMouseCellPosition();

              //  UnityEngine.Debug.Log($"IsCellExist: {_gameBoard.Grid.IsCellExist(cellPosition)}");
                //UnityEngine.Debug.Log($"IsBallExist: {_gameBoard.Grid.IsBallExist(cellPosition)}");
           // }
        }
    }
}
