using Balls.Source.Logic.GameBoard;
using Balls.Source.Logic.Score;
using Balls.Source.View.Cameras;
using Balls.Source.View.GameBoard;
using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using System.Threading;
using UnityEngine;

namespace Balls.Source
{
    public class LevelBootstrapper : MonoBehaviour, ILevelBootstrapper
    {
        private GameBoardView _gameBoardView;
        private GameCamera _gameCamera;
        private GameScore _gameScore;

        [Inject]
        private void Constructor(
            GameBoardView gameBoardView, 
            GameCamera gameCamera,
            GameScore gameScore)
        {   
            _gameCamera = gameCamera;
            _gameBoardView = gameBoardView;
            _gameScore = gameScore;
        }

        public async UniTask Bootstrap(CancellationToken token = default)
        {
            await _gameScore.Initialize();
            _gameBoardView.StartNewGame(new GridSize(9, 9));
            _gameCamera.Fit();
        }
    }
}