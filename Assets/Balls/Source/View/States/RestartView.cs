using Balls.Source.View.GameBoard;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Balls.Source.View.States
{
    public class RestartView : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        
        private BoardView _gameBoardView;
        private IInterstitialAdService _interstitialAdService;

        [Inject]
        private void Constructor(BoardView gameBoardView, IInterstitialAdService interstitialAdService)
        {
            _gameBoardView = gameBoardView;
            _interstitialAdService = interstitialAdService;
        }
        
        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClicked);

            _interstitialAdService.Closed += OnInterstitialClosed;
            _interstitialAdService.Error += OnInterstitialError;
        }

        private void OnDisable()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _interstitialAdService.Closed -= OnInterstitialClosed;
            _interstitialAdService.Error -= OnInterstitialError;
        }

        private void OnInterstitialClosed()
        {
            _gameBoardView.RestartGame();
        }

        private void OnInterstitialError(string error)
        {
            _gameBoardView.RestartGame();
        }

        private void OnRestartButtonClicked()
        {
            _interstitialAdService.OpenInterstitialAd();
        }
    }
}