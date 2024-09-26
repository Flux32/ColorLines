using Balls.Source.Logic.GameBoard;
using Balls.Source.View.GameBoard;
using Reflex.Attributes;
using UnityEngine;

public class GameBoardInputRouter : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.Mouse0) == true)
        {
            _gameBoardView.Select(_cellInput.GetMouseCellPosition(_gameBoardView.Grid.CellSize));
        }
    }
}