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

        public UniTask PlaySolve(CancellationToken cancellationToken = default)
        {
            _tween?.Kill();

            _animationSettings.PopEffect.gameObject.SetActive(true);

            _ballTransform.localScale = Vector3.zero;
            GameObject.Instantiate(_animationSettings.PopEffect, _ballTransform.position, Quaternion.identity);
            Solved?.Invoke();
            return UniTask.CompletedTask;
        }

        public void Dispose()
        {
            _tween?.Kill();
        }
    }
}