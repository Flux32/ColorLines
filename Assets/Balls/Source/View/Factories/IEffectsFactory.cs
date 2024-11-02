using Balls.Source.View.Effects;

namespace Balls.Source.View.Factories
{
    public interface IEffectsFactory
    {
        SimpleEffect CreatePopEffect();
        void ReclaimPopEffect(SimpleEffect effect);
        FloatingValueEffect CreateFloatingValueEffect();
        void ReclaimFloatingValueEffect(FloatingValueEffect effect);
    }
}