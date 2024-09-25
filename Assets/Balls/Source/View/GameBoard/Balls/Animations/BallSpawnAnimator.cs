using System;
using System.Threading;
using Balls.Source.Infrastructure.DOTween;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Balls.Source.View.GameBoard.Balls.Animations
{
    public sealed class BallSpawnAnimator : IDisposable
    {
        private readonly TweenSettings _spawnAnimationSettings;
        private readonly Transform _transform;
        
        private Tween _tween;
        
        public BallSpawnAnimator(TweenSettings spawnAnimationSettings, Transform transform)
        {
            _spawnAnimationSettings = spawnAnimationSettings;
            _transform = transform;
        }
        
        public async UniTask PlaySpawn(CancellationToken cancellationToken = default)
        {
            _tween?.Kill();

            _tween = _transform
                .DOScale(Vector3.one, _spawnAnimationSettings.Duration)
                .SetEase(_spawnAnimationSettings.Ease);
            
            await _tween.WithCancellation(cancellationToken);
        }
        
        public void Dispose()
        {
            _tween?.Kill();
        }
    }
}