using System;
using Balls.Source.Infrastructure.DOTween;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Balls.Source.View.GameBoard.Balls.Animations
{
    public class BallJumpAnimator : IDisposable
    {
        private readonly TweenSettings _tweenSettings;
        private readonly Transform _transform;
        
        private bool _jumpStopRequested;
        private Tween _tween;

        public BallJumpAnimator(TweenSettings tweenSettings, Transform transform)
        {
            _tweenSettings = tweenSettings;
            _transform = transform;
        }

        public void StartJump()
        {
            _tween?.Kill();
            
            _jumpStopRequested = false;

            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(_transform.DOMove(new Vector3(0f, 0.1f), _tweenSettings.Duration / 2))
                .Append(_transform.DOMove(new Vector3(0f, -0.1f), _tweenSettings.Duration / 2))
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