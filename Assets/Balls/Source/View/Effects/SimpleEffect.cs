using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Balls.Source.View.Effects
{
    public class SimpleEffect : MonoBehaviour, ISimpleEffect
    {
        private static readonly int PopHash = Animator.StringToHash("Pop");

        [SerializeField] private Animator _animator;

        private bool _isPopPlay;

        public async UniTask Play()
        {
            _animator.SetTrigger(PopHash);

            await UniTask.WaitWhile(() => _isPopPlay);
        }

        //Called from animator
        private void OnPopCompleted()
        {
            _isPopPlay = false;
        }
    }
}