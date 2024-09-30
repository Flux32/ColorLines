using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Balls.Source.View.UI.Elements
{
    public class ValueIndicator : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
    
        [SerializeField] private Image _icon;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private bool _translateValue = false;
        [SerializeField] private string _format = "{0}";

        [SerializeField] private float _animationScale = 1;
        [SerializeField] private float _animationScaleDuration = 0;

        private int _value;

        private Sequence _sequence;

        public event Action<int> OnUpdateView;
    
        public void SetValueWithoutAnimation(int value)
        {
            UpdateValue(value);
        }

        public async UniTask SetValue(int value)
        {
            _sequence = DOTween.Sequence();

            if (_icon != null)
            {
                await _sequence
                    .Append(_icon.transform.DOScale(_animationScale, _animationScaleDuration / 2))
                    .Append(_icon.transform.DOScale(1, _animationScaleDuration / 2))
                    .Insert(0, DOTween.To(() => _value, UpdateValue, value, _duration));
            }
            else
            {
                await _sequence
                    .Append(_label.transform.DOScale(_animationScale, _animationScaleDuration / 2))
                    .Append(_label.transform.DOScale(1, _animationScaleDuration / 2))
                    .Insert(0, DOTween.To(() => _value, UpdateValue, value, _duration));
            }
        }

        private void UpdateValue(int value)
        {
            _value = value;

            if (_translateValue == true)
                _label.text = string.Format(_format, ValueTranslator.Translate(value));
            else
                _label.text = string.Format(_format, value);
        
            OnUpdateView?.Invoke(_value);
        }

        private void OnDestroy()
        {
            _sequence.Kill();
        }
    }
}
