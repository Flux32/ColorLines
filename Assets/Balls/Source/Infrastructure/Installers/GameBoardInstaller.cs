using Reflex.Core;
using UnityEngine;

public class GameBoardInstaller : MonoBehaviour, IInstaller
{
    [SerializeField] private BallViewFactory _ballViewFactory;
    [SerializeField] private GameBoardView _gameBoardView;
    [SerializeField] private Camera _camera;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder
            .AddSingleton(typeof(GameBoardGrid))
            .AddSingleton(typeof(CellPointerInput))
            .AddSingleton(_gameBoardView)
            .AddSingleton(typeof(GameBoard))
            .AddSingleton(_ballViewFactory, typeof(IBallViewFactory))
            .AddSingleton(typeof(BallGenerator))
            .AddSingleton(_camera);
    }
}