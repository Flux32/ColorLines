using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UnityEngine;

namespace Balls.View.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private float _fadeDuration;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public UniTask Open(CancellationToken cancellationToken)
        {
            return _canvasGroup.DOFade(1, _fadeDuration)
                               .WithCancellation(cancellationToken);
        }

        public UniTask Close(CancellationToken cancellationToken)
        {
            return _canvasGroup.DOFade(0, _fadeDuration)
                               .WithCancellation(cancellationToken);
        }
    }
}