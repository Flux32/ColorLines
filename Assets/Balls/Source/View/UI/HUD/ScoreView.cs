using Balls.Source.Logic.Score;
using Balls.Source.View.UI.Elements;
using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using UnityEngine;

namespace Balls.Source.View.UI.HUD
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private ValueIndicator _bestScoreIndicator;
        [SerializeField] private ValueIndicator _scoreIndicator;

        private IGameScore _gameScore;

        [Inject]
        private void Constructor(IGameScore gameScore)
        {
            _gameScore = gameScore;
        }

        private void OnEnable()
        {
            _gameScore.ScoreInitialized += OnScoreInitialized;
            _gameScore.ScoreChanged += OnScoreChanged;
            _gameScore.BestScoreChanged += OnBestScoreChanged;
        }

        private void OnDisable()
        {
            _gameScore.ScoreInitialized -= OnScoreInitialized;
            _gameScore.ScoreChanged -= OnScoreChanged;
            _gameScore.BestScoreChanged -= OnBestScoreChanged;
        }

        private void OnBestScoreChanged(BestScore bestScore)
        {
            _bestScoreIndicator
                 .SetValue(bestScore.Value)
                 .Forget();
        }

        private void OnScoreInitialized(int currentScore, BestScore bestScore)
        {
            _scoreIndicator.SetValueWithoutAnimation(currentScore);
            _bestScoreIndicator.SetValueWithoutAnimation(bestScore.Value);
        }

        private void OnScoreChanged(int score)
        {
            _scoreIndicator
                 .SetValue(score)
                 .Forget();
        }
    }
}
