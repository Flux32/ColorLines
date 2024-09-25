using System;
using DG.Tweening;
using UnityEngine;

namespace Balls.Source.Infrastructure.DOTween
{
    [Serializable]
    public class TweenSettings
    {
        public float Duration => _duration;
        public Ease Ease => _ease;

        [SerializeField] private float _duration = 1.0f;
        [SerializeField] private Ease _ease = Ease.Linear;
    }
}