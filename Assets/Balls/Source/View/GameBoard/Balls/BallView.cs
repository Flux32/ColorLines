using System.Threading;
using Balls.Source.Core.Struct;
using Balls.Source.View.GameBoard.Balls.Animations;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Balls.Source.View.GameBoard.Balls
{
    public class BallView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
    
        [Header("Animations")]
        [SerializeField] private BallSpawnAnimationSettings _spawnAnimationSettings;
        [SerializeField] private BallJumpAnimationSettings _jumpAnimationSettings;
        [SerializeField] private BallMoveAnimationSettings _moveAnimationSettings;
        [SerializeField] private BallSolveAnimationSettings _solveAnimationSettings;
    
        [Header("Sounds")]
        [SerializeField] private BallSoundsSettings _soundsSettings;
        
        private BallSpawnAnimator _spawnAnimator;
        private BallJumpAnimator _jumpAnimator;
        private BallMoveAnimator _moveAnimator;
        private BallSolveAnimator _solveAnimator;
                
        public GridPosition CellPosition { get; set; }
        public Color AccentColor { get; private set; }
        
        private void Awake()
        {
            _spawnAnimator = new BallSpawnAnimator(transform, _spawnAnimationSettings);
            _jumpAnimator = new BallJumpAnimator(transform, _jumpAnimationSettings);
            _moveAnimator = new BallMoveAnimator(transform, _moveAnimationSettings);
            _solveAnimator = new BallSolveAnimator(transform, _solveAnimationSettings);
        }

        public void Initialize(Sprite ballSprite, Color accentColor)
        {
            _spriteRenderer.sprite = ballSprite;
            AccentColor = accentColor;
        }
    
        public async UniTask Move(Vector3[] path, CancellationToken cancellationToken = default)
        {
            await _jumpAnimator.StopJump();
            await _moveAnimator.PlayMove(path, cancellationToken);
        }
        
        public void SetSelectedState()
        {
            _jumpAnimator.StartJump();
        }
    
        public void SetUnselectedState()
        {
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

        public void TransitToHoldState()
        {
            transform.DOScale(1.1f, 0.3f);
        }

        public void TransitToNormalState()
        {
            transform.DOScale(1f, 0.3f);
        }
    }
}