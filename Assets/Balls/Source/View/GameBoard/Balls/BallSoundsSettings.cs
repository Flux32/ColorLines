using System;
using UnityEngine;

namespace Balls.Source.View.GameBoard.Balls
{
    [Serializable]
    public sealed class BallSoundsSettings
    {
        [SerializeField] private AudioClip _spawnClip;
        [SerializeField] private AudioClip _jumpLandClip;
        [SerializeField] private AudioClip _moveClip;
        [SerializeField] private AudioClip _solveClip;
        
        public AudioClip SpawnClip => _spawnClip;
        public AudioClip JumpLandClip => _jumpLandClip;
        public AudioClip MoveClip => _moveClip;
        public AudioClip SolveClip => _solveClip;
    }
}