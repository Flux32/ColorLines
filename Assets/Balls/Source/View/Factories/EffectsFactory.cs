using Balls.Source.View.Effects;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace Balls.Source.View.Factories
{
    public class EffectsFactory : MonoBehaviour, IEffectsFactory
    {
        [SerializeField] private SimpleEffect _popEffectPrefab;
        [SerializeField] private FloatingValueEffect _floatingValueEffectPrefab;
        
        private ObjectPool<SimpleEffect> _popEffectPool;
        private ObjectPool<FloatingValueEffect> _floatingValueEffectPool;

        private void Awake()
        {
            _popEffectPool = new ObjectPool<SimpleEffect>(InstantiatePopEffect);
            _floatingValueEffectPool = new ObjectPool<FloatingValueEffect>(InstantiateFloatingValueEffect);
        }
    
        public SimpleEffect CreatePopEffect()
        {
            SimpleEffect effect = _popEffectPool.Get();
            effect.gameObject.SetActive(true);
            return effect;
        }

        public void ReclaimPopEffect(SimpleEffect effect)
        {
            effect.gameObject.SetActive(false);
            _popEffectPool.Release(effect);
        }

        public FloatingValueEffect CreateFloatingValueEffect()
        {
            return _floatingValueEffectPool.Get();
        }

        public void ReclaimFloatingValueEffect(FloatingValueEffect effect)
        {
            effect.gameObject.SetActive(false);
            _floatingValueEffectPool.Release(effect);
        }

        private FloatingValueEffect InstantiateFloatingValueEffect()
        {
            return Instantiate(_floatingValueEffectPrefab);
        }
        
        private SimpleEffect InstantiatePopEffect()
        {
            return Instantiate(_popEffectPrefab);
        }
    }
}
