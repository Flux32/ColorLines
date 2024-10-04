using Balls.Source.View.GameBoard;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Balls.Source.View.States
{
    public class RestartView : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        
        private GameBoardView _gameBoardView;

        [Inject]
        private void Constructor(GameBoardView gameBoardView)
        {
            _gameBoardView = gameBoardView;
        }
        
        private void OnEnable()
        {
            _restartButton.onClick.AddListener(_gameBoardView.RestartGame);
        }

        private void OnDisable()
        {
            _restartButton.onClick.AddListener(_gameBoardView.RestartGame);
        }
    }
}