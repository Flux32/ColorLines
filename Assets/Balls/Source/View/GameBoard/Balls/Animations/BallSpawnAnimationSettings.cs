using System;
using DG.Tweening;
using UnityEngine;

namespace Balls.Source.View.GameBoard.Balls.Animations
{
    [Serializable]
    public sealed class BallSpawnAnimationSettings
    {
        [SerializeField] private float _spawnDuration = 0.5f;
        [SerializeField] private Ease _spawnEase = Ease.Linear;
        
        public float SpawnDuration => _spawnDuration;
        public Ease SpawnEase => _spawnEase;
    }
}