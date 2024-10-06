using Balls.Source.Infrastructure.Repositories;
using Balls.Source.Logic.GameBoard;
using Balls.Source.Logic.Score;
using Balls.Source.View.Cameras;
using Balls.Source.View.Factories;
using Balls.Source.View.GameBoard;
using Balls.Source.View.GameBoard.Input;
using Reflex.Core;
using UnityEngine;

namespace Balls.Source.Infrastructure.Installers
{
    public class GameplayInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private BallViewFactory _ballViewFactory;
        [SerializeField] private GameBoardView _gameBoardView;
        [SerializeField] private GameCamera _gameCamera;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingleton(typeof(CellPointerInput))
                .AddSingleton(_gameBoardView)
                .AddSingleton(typeof(GameBoard))
                .AddSingleton(_ballViewFactory, typeof(IBallViewFactory))
                .AddSingleton(_gameCamera)
                .AddSingletonInterfaces(typeof(GameScore))
                .AddSingletonInterfaces(typeof(BestScoreRepository));
        }
    }
}