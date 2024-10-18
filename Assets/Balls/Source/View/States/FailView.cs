using Balls.Source.View.UI.HUD;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Balls.Source.View.States
{
    public class FailView : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private RestartButtonAnimations _restartButtonAnimations;
        
        private Logic.GameBoard.Board _gameBoard;

        [Inject]
        private void Constructor(Logic.GameBoard.Board gameBoard)
        {
            _gameBoard = gameBoard;
        }

        private void OnEnable()
        {
            _gameBoard.Filled += PlayFail;
        }

        private void OnDisable()
        {
            _gameBoard.Filled -= PlayFail;
        }
        
        private void PlayFail()
        {
            _restartButtonAnimations.FadeToAccentColor();
        }
    }
}
