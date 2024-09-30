using System;
using Balls.Source.Logic.GameBoard;
using Balls.Source.Logic.GameBoard.Operations;
using Reflex.Attributes;
using UnityEngine;

namespace Balls.Source.Logic.Score
{
    public class ScoreCounter : MonoBehaviour
    {
        public event Action ScoreChanged;

        private int _score;
        private GameBoard.GameBoard _gameBoard;

        private ScoreSettings _scoreSettings = new ScoreSettings(1, 6);

        [Inject]
        private void Constructor(GameBoard.GameBoard gameBoard)
        {
            _gameBoard = gameBoard;
        }
    
        private void OnEnable()
        {
            _gameBoard.Moved += OnMove;
        }

        private void OnDisable()
        {
            _gameBoard.Moved -= OnMove;
        }

        private void OnMove(MoveOperationResult moveOperationResult)
        {
    
        }
    }

    public sealed class ScoreSettings
    {
        public ScoreSettings(int scoreForBall, int startFromBallIncrement)
        {
            ScoreForBall = scoreForBall;
            StartFromBallIncrement = startFromBallIncrement;
        }

        public int ScoreForBall { get; private set; }
        public int StartFromBallIncrement { get; private set; }
    }
}