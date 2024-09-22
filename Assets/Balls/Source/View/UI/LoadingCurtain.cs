using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UnityEngine;

namespace Balls.View.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        [SerializeField] private float _fadeDuration;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public async UniTask Open(CancellationToken cancellationToken)
        {
            gameObject.SetActive(true);

            await _canvasGroup.DOFade(1, _fadeDuration)
                               .WithCancellation(cancellationToken);
        }

        public async UniTask Close(CancellationToken cancellationToken)
        {
            await _canvasGroup.DOFade(0, _fadeDuration)
                              .WithCancellation(cancellationToken);
            
            gameObject.SetActive(false);
        }

        public void SetOpenedState()
        {
            _canvasGroup.alpha = 1.0f;
            gameObject.SetActive(true);
        }    
    }
}