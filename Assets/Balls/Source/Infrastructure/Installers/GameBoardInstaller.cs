using Balls.Source.Logic.GameBoard;
using Balls.Source.Logic.GameBoard.Generators;
using Balls.Source.View.Factories;
using Balls.Source.View.GameBoard;
using Reflex.Core;
using UnityEngine;
using Grid = Balls.Source.Logic.GameBoard.Grid;

public class GameBoardInstaller : MonoBehaviour, IInstaller
{
    [SerializeField] private BallViewFactory _ballViewFactory;
    [SerializeField] private GameBoardView _gameBoardView;
    [SerializeField] private Camera _camera;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder
            .AddSingleton(typeof(CellPointerInput))
            .AddSingleton(_gameBoardView)
            .AddSingleton(typeof(GameBoard))
            .AddSingleton(_ballViewFactory, typeof(IBallViewFactory))
            .AddSingleton(typeof(RandomBallGenerator))
            .AddSingleton(_camera);
    }
}