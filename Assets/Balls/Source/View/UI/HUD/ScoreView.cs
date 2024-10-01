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
          
               _gameScore.ScoreChanged += OnScoreChanged;
               _gameScore.BestScoreChanged += OnBestScoreChanged;
          }
     
          private void OnBestScoreChanged(BestScore bestScore)
          {
               _bestScoreIndicator
                    .SetValue(bestScore.Score)
                    .Forget();
          }

          private void OnScoreChanged(int score)
          {
               _scoreIndicator
                    .SetValue(score)
                    .Forget();
          }
     }
}
