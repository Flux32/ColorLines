using Balls.Core;
using DG.Tweening;
using Reflex.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameBoardView : MonoBehaviour 
{
    private IBallViewFactory _ballFactory;

    private GameBoard _gameBoard;

    private BallView[,] _ballViews = new BallView[5,5];

    private BallView _selectedBall;

    [Inject]
    private void Constructor(GameBoard gameBoard, IBallViewFactory ballViewFactory)
    {
        _gameBoard = gameBoard;
        _ballFactory = ballViewFactory;
    }

    private void OnEnable()
    {
        _gameBoard.BallsPlaced += OnBallPlaced;
        _gameBoard.BallMoved += OnBallMoved;
    }

    private void OnDisable()
    {
        _gameBoard.BallsPlaced -= OnBallPlaced;
        _gameBoard.BallMoved -= OnBallMoved;
    }

    private void OnBallMoved(Path path)
    {
        Debug.Log("Nice");

        GridPosition fromPosition = path.Points[0];
        GridPosition toPosition = path.Points.Last();

        Sequence sequence = DOTween.Sequence();

        BallView ballView = _ballViews[fromPosition.X, fromPosition.Y];
        _ballViews[fromPosition.X, fromPosition.Y] = null;
        _ballViews[toPosition.X, toPosition.Y] = ballView;
         ballView.CellPosition = toPosition.ToVector2Int();
        
        foreach (GridPosition position in path.Points)
            sequence.Append(ballView.transform.DOMove(position.ToVector2(), 0.3f).SetEase(Ease.Linear));
    }

    private void OnBallPlaced(IEnumerable<Ball> balls)
    {
        foreach (Ball ball in balls)
        {
            BallView ballView = _ballFactory.CreateBall(ball.Id);
            ballView.CellPosition = ball.Position.ToVector2Int();
            ballView.MoveToCellPosition();
            _ballViews[ball.Position.X, ball.Position.Y] = ballView;
        }
    }

    public void Select(Vector2Int position)
    {
        BallView ballView = _ballViews[position.x, position.y];

        Debug.Log("Selected cell is null");

        if (ballView == null)
        {
            if (_selectedBall != null)
            {
                _selectedBall.Unselect();
                Vector2Int previousPosition = _selectedBall.CellPosition;
                _selectedBall = null;
                _gameBoard.MakeMove(new GridPosition(previousPosition.x, previousPosition.y),
                    new GridPosition(position.x, position.y));
            }

            return;
        }

        if (ballView == _selectedBall)
            return;

        if (_selectedBall != null)
            _selectedBall.Unselect();

        ballView.Select();
        _selectedBall = ballView;
    }
}