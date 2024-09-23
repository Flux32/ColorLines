using DG.Tweening;
using System;
using Balls.Source.Core.Struct;
using Balls.Source.Infrastructure.Extensions;
using UnityEngine;

public class BallView : MonoBehaviour
{
    [SerializeField] private float _hintScale;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private bool _selected;

    private Sequence _jumpSequence;
    private bool _animationStopRequested;

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

    public void Unselect()
    {
        _animationStopRequested = true;
        _selected = false;
    }

    public void UnselectWithoutAnimation()
    {
        _jumpSequence?.Kill();
    }    

    public void SetHintState()
    {
        transform.localScale = Vector3.one * _hintScale;
    }

    public void SetNormalState()
    {
        transform.localScale = Vector3.one;
    }

    public void PlayScaleToNormalSize()
    {
    }

    public void MoveToCellPosition()
    {
        transform.position = CellPosition.ToVector3();
    }

    private void OnDestroy()
    {
        _jumpSequence?.Kill();
    }
}