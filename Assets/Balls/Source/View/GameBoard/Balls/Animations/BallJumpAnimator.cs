using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Balls.Source.View.GameBoard.Balls.Animations
{
    public sealed class BallJumpAnimator : IDisposable
    {
        private readonly Transform _ballTransform;
        private readonly BallJumpAnimationSettings _animationSettings;
        
        private bool _jumpStopRequested;
        private Tween _tween;
        
        public BallJumpAnimator(Transform ballTransform, BallJumpAnimationSettings animationSettings)
        {
            _ballTransform = ballTransform;
            _animationSettings = animationSettings;
        }
        
        public event Action Landed;
        
        public void StartJump()
        {
            _tween?.Kill();
            
            _jumpStopRequested = false;

            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(_ballTransform
                            .DOMove(new Vector3(0f, _animationSettings.JumpHeight), _animationSettings.JumpDuration)
                            .SetEase(_animationSettings.FallEase))
                .Append(_ballTransform
                            .DOMove(new Vector3(0f, -_animationSettings.JumpHeight), _animationSettings.FallDuration)
                            .SetEase(_animationSettings.FallEase))
                .AppendCallback(() => Landed?.Invoke())
                .SetRelative()
                .SetLoops(-1)
                .OnStepComplete(() =>
                    {
                        if (_jumpStopRequested == true)
                            _tween?.Kill();
                    }
                );

            _tween = sequence;
        }

        public async UniTask StopJump()
        {
            _jumpStopRequested = true;
            await UniTask.WaitUntil(() => _tween == null || _tween.IsActive() == false);
        }
        
        public void Dispose()
        {
            _tween?.Kill();
        }
    }
}