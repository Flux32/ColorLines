using System.Threading;
using Balls.Source.Core.Struct;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Balls.Source.View.GameBoard.Balls
{
    public class BallView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
    
        [Header("Animations")]
        [SerializeField] private float _hintScale;
        [SerializeField] private float _spawnAnimationDuration = 0.5f;
        [SerializeField] private float _solveAnimationDuration = 0.3f;
        [SerializeField] private float _moveSpeed = 10;
    
        private bool _animationStopRequested;
        private Sequence _jumpSequence;
        private bool _selected;
    
        public GridPosition CellPosition { get; set; }
    
        public void Initialize(Sprite ballSprite)
        {
            _spriteRenderer.sprite = ballSprite;
        }
    
        public void Select()
        {
            if (_selected == true)
                return;

            _animationStopRequested = false;

            _jumpSequence = DOTween.Sequence();

            _jumpSequence
                .Append(transform.DOMove(new Vector3(0f, 0.1f), 0.2f))
                .Append(transform.DOMove(new Vector3(0f, -0.1f), 0.1f))
                .SetRelative()
                .SetLoops(-1)
                .OnStepComplete(() =>
                    {
                        if (_animationStopRequested == true)
                            _jumpSequence?.Kill();
                    }
                );

            _selected = true;
        }

        public UniTask Move(Vector3[] path, CancellationToken cancellationToken = default)
        {
            _animationStopRequested = true;
            _jumpSequence?.Kill();
            return transform.DOPath(path, _moveSpeed)
                .SetEase(Ease.Linear)
                .SetSpeedBased()
                .WithCancellation(cancellationToken);
        }
    
        public void Unselect()
        {
            _animationStopRequested = true;
            _selected = false;
        }
    
        public void SetUnspawnedState()
        {
            transform.localScale = Vector3.zero;
        }

        public async UniTask PlaySpawnAnimation()
        {
            await transform
                .DOScale(Vector3.one, _spawnAnimationDuration)
                .ToUniTask();
        }

        public async UniTask PlaySolveAnimation()
        {
            await transform
                .DOScale(Vector3.zero, _solveAnimationDuration)
                .ToUniTask();
        }

        private void OnDestroy()
        {
            _jumpSequence?.Kill();
        }
    }
}