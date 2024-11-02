using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Balls.Source.View.GameBoard.Balls.Animations
{
    public sealed class BallSolveAnimator : IDisposable
    {
        private readonly Transform _ballTransform;
        private readonly BallSolveAnimationSettings _animationSettings;

        private Tween _tween;

        public BallSolveAnimator(Transform ballTransform, BallSolveAnimationSettings animationSettings)
        {
            _ballTransform = ballTransform;
            _animationSettings = animationSettings;
        }
        
        public async UniTask PlaySolve(CancellationToken cancellationToken = default)
        {
            _tween?.Kill();
            await _ballTransform.DOScale(1.1f, _animationSettings.SolveDuration)
                                .SetEase(_animationSettings.SolveEase).WithCancellation(cancellationToken);
            
            _ballTransform.localScale = Vector3.zero;
        }

        public void Dispose()
        {
            _tween?.Kill();
        }
    }
}