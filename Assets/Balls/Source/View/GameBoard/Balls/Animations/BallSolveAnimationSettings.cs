using System;
using DG.Tweening;
using UnityEngine;

namespace Balls.Source.View.GameBoard.Balls.Animations
{
    [Serializable]
    public sealed class BallSolveAnimationSettings
    {
        [SerializeField] private Animator _popEffect;
        [SerializeField] private float _solveDuration = 0.2f;
        [SerializeField] private Ease _solveEase = Ease.Linear;
        
        public float SolveDuration => _solveDuration;
        public Ease SolveEase => _solveEase;
        public Animator PopEffect => _popEffect;
    }
}