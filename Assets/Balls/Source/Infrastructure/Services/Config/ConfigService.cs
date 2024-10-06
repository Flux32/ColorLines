using Balls.Source.Logic.GameBoard;
using Balls.Source.Logic.Score;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Balls.Source.Infrastructure.Services.Config
{
    public class GameBoardSettings
    {
        public GameBoardSettings(GridSize gridSize, int generationBallsAmount, int needBallsToSolveAmount)
        {
            GridSize = gridSize;
            GenerationBallsAmount = generationBallsAmount;
            NeedBallsToSolveAmount = needBallsToSolveAmount;
        }

        public GridSize GridSize { get; private set; }
        public int GenerationBallsAmount { get; private set; }
        public int NeedBallsToSolveAmount { get; private set; }
    }
    
    public class ConfigService : IConfigService
    {
        private const string ConfigPath = "GameConfig"; 
        
        public GameBoardSettings GameBoardSettings { get; private set; }
        public ScoreSettings ScoreSettings { get; private set; }

        public async UniTask Load()
        {
            GameConfig config = (GameConfig)await Resources.LoadAsync<GameConfig>(ConfigPath);
            GridSize gridSize = new GridSize(config.GridSize.x, config.GridSize.y);
            
            ScoreSettings = new ScoreSettings(config.ScoreForBall, config.StartIncreaseWhenSolveAmount);
            GameBoardSettings = new GameBoardSettings(gridSize, config.GenerationBallsAmount, config.MinBallsToSolveAmount);
        }
    }
}