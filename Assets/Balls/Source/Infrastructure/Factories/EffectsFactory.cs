using Balls.Source.View.Effects;
using UnityEngine;

public class SimpleEffectsFactory
{
    [SerializeField] private SimpleEffect _popPrefab;

    public SimpleEffect PopEffect()
    {
        return _popPrefab;
    }
}