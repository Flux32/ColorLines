using System;
using Balls.Source.Infrastructure.Data.Repositories;
using Balls.Source.Logic.GameBoard.Operations;

namespace Balls.Source.Logic.Score
{
    public class GameScore : IGameScore
    {
        private int _score;

        private readonly IBestScoreRepository _bestScoreRepository;
        private readonly GameBoard.GameBoard _gameBoard;
        
        public GameScore(IBestScoreRepository bestScoreRepository, GameBoard.GameBoard gameBoard)
        {
            _bestScoreRepository = bestScoreRepository;
            _gameBoard = gameBoard;
            
            _gameBoard.Moved += OnGameBoardMoved;
        }

        private void OnGameBoardMoved(MoveOperationResult moveOperationResult)
        {
            int scoreSum = moveOperationResult.SolvedBallsAfterMove.SolveScore.SumScore;

            foreach (SolveResult solveResult in moveOperationResult.SolvedBallsAfterGeneration)
                scoreSum += solveResult.SolveScore.SumScore;
            
            AddScore(scoreSum);
        }

        public event Action<int> ScoreChanged;
        public event Action<BestScore> BestScoreChanged;
        
        public BestScore BestScore { get; private set; } = new BestScore(DateTime.Now, 0);
        public int CurrentScore { get; private set; }
        
        private void AddScore(int score)
        {
            CurrentScore += score;
            ScoreChanged?.Invoke(CurrentScore);

            if (BestScore.Value >= CurrentScore) 
                return;
            
            BestScore = new BestScore(DateTime.Now, CurrentScore);
            _bestScoreRepository.Set(BestScore);
            BestScoreChanged?.Invoke(BestScore);
        }
    }
}