using System;
using DG.Tweening;
using UnityEngine;

namespace Balls.Source.View.GameBoard.Balls.Animations
{
    [Serializable]
    public sealed class BallMoveAnimationSettings
    {
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private Ease _moveEase = Ease.Linear;
        
        public float MoveSpeed => _moveSpeed;
        public Ease MoveEase => _moveEase;
    }
}