using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Balls.Source.View.UI.HUD
{
    public class RestartButtonAnimations : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _fadeDuration = 1f;
        [SerializeField] private Color _accentColor = new Color(0x66, 0x9A, 0xFF, 0xFF);

        private Color _normalColor;

        private Tween _fadeTween;
        
        private void Awake()
        {
            _normalColor = _image.color;
        }

        public void FadeToAccentColor()
        {
            _fadeTween?.Kill();
            _fadeTween = _image.DOColor(_accentColor, _fadeDuration);
        }

        public void FadeToNormalColor()
        {
            _fadeTween?.Kill();
            _fadeTween = _image.DOColor(_normalColor, _fadeDuration);
        }

        private void OnDestroy()
        {
            _fadeTween?.Kill();
        }
    }
}
