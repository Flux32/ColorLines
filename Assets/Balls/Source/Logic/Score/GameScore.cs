using System;
using Balls.Source.Infrastructure.Data.Repositories;
using Balls.Source.Logic.GameBoard.Operations;
using Cysharp.Threading.Tasks;
using UnityEngine.Animations;

namespace Balls.Source.Logic.Score
{
    public class GameScore : IGameScore, IDisposable
    {
        private readonly IBestScoreRepository _bestScoreRepository;
        private readonly GameBoard.Board _gameBoard;
        
        public GameScore(IBestScoreRepository bestScoreRepository, GameBoard.Board gameBoard)
        {
            _bestScoreRepository = bestScoreRepository;
            _gameBoard = gameBoard;
            
            _gameBoard.Moved += OnGameBoardMoved;
        }

        public event Action<int, BestScore> ScoreInitialized;
        public event Action<int> ScoreChanged;
        public event Action<BestScore> BestScoreChanged;

        public BestScore BestScore { get; private set; } = new BestScore(DateTime.Now, 0);
        public int CurrentScore { get; private set; }

        public async UniTask Initialize()
        {
            BestScore = await _bestScoreRepository.Get();
            ScoreInitialized?.Invoke(CurrentScore, BestScore);
        }

        private void OnGameBoardMoved(MoveOperationResult moveOperationResult)
        {
            int scoreSum = moveOperationResult.SolvedBallsAfterMove.SolveScore.SumScore;

            foreach (SolveResult solveResult in moveOperationResult.SolvedBallsAfterGeneration)
                scoreSum += solveResult.SolveScore.SumScore;

            AddScore(scoreSum);
        }

        private void AddScore(int score)
        {
            CurrentScore += score;
            ScoreChanged?.Invoke(CurrentScore);

            if (BestScore.Value >= CurrentScore) 
                return;
            
            BestScore = new BestScore(DateTime.Now, CurrentScore);
            _bestScoreRepository.Set(BestScore).Forget();
            BestScoreChanged?.Invoke(BestScore);
        }

        public void Dispose()
        {
            _gameBoard.Moved -= OnGameBoardMoved;
        }
    }
}