using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Balls.Source.View.GameBoard.Balls.Animations
{
    public sealed class BallSpawnAnimator : IDisposable
    {
        private readonly Transform _ballTransform;
        private readonly BallSpawnAnimationSettings _animationSettings;
        private Tween _tween;
        
        public BallSpawnAnimator(Transform ballTransform, BallSpawnAnimationSettings animationSettings)
        {
            _ballTransform = ballTransform;
            _animationSettings = animationSettings;
        }
        
        public event Action Spawned;
        
        public async UniTask PlaySpawn(CancellationToken cancellationToken = default)
        {
            _tween?.Kill();

            Spawned?.Invoke();
            
            _tween = _ballTransform
                        .DOScale(Vector3.one, _animationSettings.SpawnDuration)
                        .SetEase(_animationSettings.SpawnEase);
            
            await _tween.WithCancellation(cancellationToken);
        }
        
        public void Dispose()
        {
            _tween?.Kill();
        }
    }
}