using Reflex.Attributes;
using UnityEngine;

public class GameBoardInputRouter : MonoBehaviour
{
    private CellPointerInput _cellInput;
    private GameBoard _gameBoard;
    private GameBoardView _gameBoardView;

    [Inject]
    private void Constructor(GameBoard gameBoard, GameBoardView gameBoardView, CellPointerInput cellInput)
    {
        _gameBoard = gameBoard;
        _gameBoardView = gameBoardView;
        _cellInput = cellInput;
    }

    private void Start()
    {
        _gameBoard.InitializeGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) == true)
        {
            _gameBoardView.Select(_cellInput.GetMouseCellPosition());
        }
    }
}