using System;
using System.Threading;
using Balls.Source.Infrastructure.DOTween;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Balls.Source.View.GameBoard.Balls.Animations
{
    public class BallSolveAnimator : IDisposable
    {
        private readonly TweenSettings _solveAnimationSettings;
        private readonly Transform _transform;
        
        private Tween _tween;

        public BallSolveAnimator(TweenSettings solveAnimationSettings, Transform transform)
        {
            _solveAnimationSettings = solveAnimationSettings;
            _transform = transform;
        }

        public async UniTask PlaySolve(CancellationToken cancellationToken = default)
        {
            _tween?.Kill();
            
            _tween = _transform
                .DOScale(Vector3.zero, _solveAnimationSettings.Duration)
                .SetEase(_solveAnimationSettings.Ease);
            
            await _tween.WithCancellation(cancellationToken);
        }

        public void Dispose()
        {
            _tween?.Kill();
        }
    }
}