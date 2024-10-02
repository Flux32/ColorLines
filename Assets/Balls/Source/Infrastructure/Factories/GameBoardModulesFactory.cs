using Balls.Source.Infrastructure.Services.Config;
using Balls.Source.Logic.GameBoard;
using Balls.Source.Logic.GameBoard.Detectors;
using Balls.Source.Logic.GameBoard.Generators;
using Balls.Source.Logic.GameBoard.Pathfinding;
using Balls.Source.Logic.GameBoard.Solvers;
using Balls.Source.Logic.Score;

namespace Balls.Source.Infrastructure.Factories
{
    public class GameBoardModulesFactory : IGameBoardModulesFactory
    {
        private readonly IConfigService _configService;

        public GameBoardModulesFactory(IConfigService configService)
        {
            _configService = configService;
        }
        
        public IBallGenerator CreateBallGenerator()
        {
            return new RandomBallGenerator(_configService.GameBoardSettings.GenerationBallsAmount);
        }

        public ISolver CreateSolver()
        {
            return new ClassicSolver(
                new LineDetector(_configService.GameBoardSettings.NeedBallsToSolveAmount), 
                new ScoreCalculator(_configService.ScoreSettings));
        }

        public IPathfinder CreatePathfinder()
        {
            GridSize gridSize = _configService.GameBoardSettings.GridSize;
            int maxOperationAmount = gridSize.Width * gridSize.Height;
            return new Pathfinder(maxOperationAmount);
        }
    }

}