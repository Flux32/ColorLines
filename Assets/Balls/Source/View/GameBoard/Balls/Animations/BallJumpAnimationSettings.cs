using System;
using DG.Tweening;
using UnityEngine;

namespace Balls.Source.View.GameBoard.Balls.Animations
{
    [Serializable]
    public sealed class BallJumpAnimationSettings
    {
        [SerializeField] private float _jumpDuration = 0.3f;
        [SerializeField] private Ease _jumpEase = Ease.Linear;
        [SerializeField] private float _fallDuration = 0.2f;
        [SerializeField] private Ease _fallEase = Ease.Linear;
        [SerializeField] private float _jumpHeight = 0.1f;
        
        public float JumpDuration => _jumpDuration;
        public Ease JumpEase => _jumpEase;
        public float FallDuration => _fallDuration;
        public Ease FallEase => _fallEase;
        public float JumpHeight => _jumpHeight;
    }
}