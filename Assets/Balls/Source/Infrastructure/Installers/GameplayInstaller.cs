using Balls.Source.Infrastructure.Data.Repositories;
using Balls.Source.Infrastructure.Data.Storages;
using Balls.Source.Logic.GameBoard;
using Balls.Source.Logic.Score;
using Balls.Source.View.Cameras;
using Balls.Source.View.Factories;
using Balls.Source.View.GameBoard;
using Balls.Source.View.GameBoard.Grid;
using Balls.Source.View.GameBoard.Input;
using Reflex.Core;
using UnityEngine;

namespace Balls.Source.Infrastructure.Installers
{
    public class GameplayInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private BallViewFactory _ballViewFactory;
        [SerializeField] private GridView _gridView;
        [SerializeField] private GameCamera _gameCamera;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingleton(typeof(CellPointerInput))
                .AddSingleton(typeof(BoardView))
                .AddSingleton(typeof(Board))
                .AddSingleton(_ballViewFactory, typeof(IBallViewFactory))
                .AddSingleton(_gameCamera)
                .AddSingleton(_gridView)
                .AddSingletonSelfAndInterfaces(typeof(GameScore))
                .AddSingletonInterfaces(typeof(BestScoreRepository))
                .AddSingletonInterfaces(typeof(PlayerPrefsJsonStorage));
        }
    }
}