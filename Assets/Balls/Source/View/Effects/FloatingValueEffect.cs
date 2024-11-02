using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Balls.Source.View.Effects
{
    [RequireComponent(typeof(RectTransform))]
    public class FloatingValueEffect : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        
        [Header("Animation")]
        [SerializeField] private string _format = "{0}";
        [SerializeField] private AnimationCurve _scaleCurve;
        [SerializeField] private float _startScale = 0.5f;
        [SerializeField] private Vector2 _minMaxTargetScale = new Vector2(1, 3);
        [SerializeField, Min(0)] private int _maxScaleWhenValue = 100;
        [SerializeField] private float _spawnDuration = 0.5f;
        [SerializeField] private float _idleDuration = 1f;
        [SerializeField] private float _moveDuration = 0.7f;
        [SerializeField] private Vector3 _moveOffset = new Vector3(0f, 1f);
        
        public async UniTask Play(int value)
        {
            _label.text = string.Format(_format, value);
            _label.alpha = 1;

            float percent = Mathf.InverseLerp(0, _maxScaleWhenValue, value);
            float scale = Mathf.Lerp(_minMaxTargetScale.x, _minMaxTargetScale.y, _scaleCurve.Evaluate(percent));

            transform.localScale = Vector3.one * _startScale;

            CancellationToken token = this.GetCancellationTokenOnDestroy();
            
            await transform.DOScale(scale, _spawnDuration)
                .WithCancellation(token);

            await UniTask.WaitForSeconds(_idleDuration, cancellationToken: token);
            
            await UniTask.WhenAll(
                transform.DOMove(transform.position + _moveOffset, _moveDuration)
                         .ToUniTask(cancellationToken: token),
                _label.DOFade(0, _moveDuration)
                      .ToUniTask(cancellationToken: token));
        }
    }
}