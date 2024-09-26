using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Balls.Source.View.GameBoard.Balls.Animations
{
    public sealed class BallMoveAnimator : IDisposable
    {
        private readonly Transform _ballTransform;
        private readonly BallMoveAnimationSettings _animationSettings;
        
        private Tween _tween;

        public BallMoveAnimator(Transform ballTransform, BallMoveAnimationSettings animationSettings)
        {
            _ballTransform = ballTransform;
            _animationSettings = animationSettings;
        }

        public async UniTask PlayMove(Vector3[] path, CancellationToken cancellationToken = default)
        {
            _tween?.Kill();
            
            _tween = _ballTransform
                        .DOPath(path, _animationSettings.MoveSpeed)
                        .SetEase(_animationSettings.MoveEase)
                        .SetSpeedBased();
            
            await _tween
                .WithCancellation(cancellationToken);
        }
        
        public void Dispose()
        {
            _tween?.Kill();
        }
    }
}