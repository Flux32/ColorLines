using Balls.Source.Logic.GameBoard;
using Balls.Source.View.Cameras;
using Balls.Source.View.GameBoard;
using Reflex.Attributes;
using UnityEngine;

namespace Balls.Source
{
    public class LevelBootstrapper : MonoBehaviour, ILevelBootstrapper
    {
        private GameBoardView _gameBoardView;
        private GameCamera _gameCamera;

        [Inject]
        private void Constructor(GameBoardView gameBoardView, GameCamera gameCamera)
        {   
            _gameCamera = gameCamera;
            _gameBoardView = gameBoardView;
        }

        private void Start()
        {
            Bootstrap();
        }

        public void Bootstrap()
        {
            _gameBoardView.StartNewGame(new GridSize(9, 9));
            _gameCamera.Fit();
        }
    }
}