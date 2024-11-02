using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Balls.Source.View.Effects
{
    public class SimpleEffect : MonoBehaviour
    {
        private static readonly int PopHash = Animator.StringToHash("Play");

        [SerializeField] private Animator _animator;

        private bool _isEffectCompleted;

        public async UniTask Play()
        {
            _animator.SetTrigger(PopHash);

            await UniTask.WaitWhile(() => _isEffectCompleted);
        }

        //Called from animator
        private void OnEffectCompleted()
        {
            _isEffectCompleted = false;
        }
    }
}