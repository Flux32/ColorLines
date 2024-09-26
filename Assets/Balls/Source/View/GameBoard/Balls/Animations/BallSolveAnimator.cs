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

        public event Action Solved;
        
        public async UniTask PlaySolve(CancellationToken cancellationToken = default)
        {
            _tween?.Kill();
            
            _tween = _ballTransform
                        .DOScale(Vector3.zero, _animationSettings.SolveDuration)
                        .SetEase(_animationSettings.SolveEase);
            
            await _tween.WithCancellation(cancellationToken);
            Solved?.Invoke();
        }

        public void Dispose()
        {
            _tween?.Kill();
        }
    }
}