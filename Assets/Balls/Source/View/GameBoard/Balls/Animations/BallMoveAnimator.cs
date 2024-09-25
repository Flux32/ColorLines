using System;
using System.Threading;
using Balls.Source.Infrastructure.DOTween;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Balls.Source.View.GameBoard.Balls.Animations
{
    public class BallMoveAnimator : IDisposable
    {
        private readonly TweenSettings _moveSettings;
        private readonly Transform _transform;

        private Tween _tween;
        
        public BallMoveAnimator(TweenSettings moveSettings, Transform transform)
        {
            _moveSettings = moveSettings;
            _transform = transform;
        }

        public async UniTask PlayMove(Vector3[] path, CancellationToken cancellationToken = default)
        {
            _tween?.Kill();
            
            _tween = _transform
                        .DOPath(path, _moveSettings.Duration)
                        .SetEase(_moveSettings.Ease)
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