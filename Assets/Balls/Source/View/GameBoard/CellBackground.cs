using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Balls.Source.View.GameBoard
{
    public class CellBackground : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        [FormerlySerializedAs("_holdColor")]
        [Header("Animations")]
        [SerializeField] private Color _holdDefaultColor = Color.white;
        [SerializeField, Min(0)] private float _holdScale = 1.1f;
        [SerializeField] private Color _pressedColor = Color.red;
        [SerializeField] private float _pressedScale = 0.8f;

        private Color _normalColor;

        private void Awake()
        {
            _normalColor = _spriteRenderer.color;
        }

        public void TransitToHoldState(Color color)
        {
            _spriteRenderer.transform.DOScale(_holdScale, 0.3f);
            _spriteRenderer.DOColor(color, 0.3f);
        }

        public void TransitToHoldState()
        {
            TransitToHoldState(_holdDefaultColor);
        }
        
        public void TransitToPressedState()
        {
            _spriteRenderer.DOColor(_pressedColor, 0.1f);
            _spriteRenderer.transform.DOScale(_pressedScale, 0.1f);
        }
        
        public void TransitFromHoldToNormalState()
        {
            _spriteRenderer.transform.DOScale(1f, 0.3f);
            _spriteRenderer.DOColor(_normalColor, 0.3f);
        }

        public void TransitFromPressedStateToHoldedState()
        {
            _spriteRenderer.transform
                .DOScale(_holdScale, 0.3f)
                .SetEase(Ease.OutBack);
        }
    }
}