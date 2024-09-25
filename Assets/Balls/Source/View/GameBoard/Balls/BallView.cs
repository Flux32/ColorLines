using System;
using System.Threading;
using Balls.Source.Core.Struct;
using Balls.Source.Infrastructure.DOTween;
using Balls.Source.View.GameBoard.Balls.Animations;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Balls.Source.View.GameBoard.Balls
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BallView : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
    
        [Header("Animations")]
        [SerializeField] private TweenSettings _spawnAnimationSettings;
        [SerializeField] private TweenSettings _jumpAnimationSettings;
        [SerializeField] private TweenSettings _moveAnimationSettings;
        [SerializeField] private TweenSettings _solveAnimationSettings;
        
        private BallSpawnAnimator _spawnAnimator;
        private BallJumpAnimator _jumpAnimator;
        private BallMoveAnimator _moveAnimator;
        private BallSolveAnimator _solveAnimator;
        
        private bool _selected;
        
        public GridPosition CellPosition { get; set; }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spawnAnimator = new BallSpawnAnimator(_spawnAnimationSettings, transform);
            _jumpAnimator = new BallJumpAnimator(_jumpAnimationSettings, transform);
            _moveAnimator = new BallMoveAnimator(_moveAnimationSettings, transform);
            _solveAnimator = new BallSolveAnimator(_solveAnimationSettings, transform);
        }
        
        public void SetBallSprite(Sprite ballSprite)
        {
            _spriteRenderer.sprite = ballSprite;
        }
    
        public async UniTask Move(Vector3[] path, CancellationToken cancellationToken = default)
        {
            await _jumpAnimator.StopJump();
            await _moveAnimator.PlayMove(path, cancellationToken);
        }
        
        public void Select()
        {
            if (_selected == true)
                return;

            _selected = true;
            _jumpAnimator.StartJump();
        }
    
        public void Unselect()
        {
            _selected = false;
            _jumpAnimator.StopJump().Forget(); //TODO: for move
        }
    
        public void SetUnspawnedState()
        {
            transform.localScale = Vector3.zero;
        }

        public UniTask PlaySpawnAnimation()
        {
            return _spawnAnimator.PlaySpawn();
        }

        public UniTask PlaySolveAnimation()
        {
            return _solveAnimator.PlaySolve();
        }
    }
}