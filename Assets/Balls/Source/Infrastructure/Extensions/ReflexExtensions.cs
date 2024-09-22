using System;
using System.Linq;
using Reflex.Core;

public static class ReflexExtensions
{
    public static ContainerBuilder AddSingletonSelfAndInterfaces(this ContainerBuilder builder, object instance)
    {
        Type instanceType = instance.GetType();
        Type[] interfacesTypes = instanceType.GetInterfaces();
        Type[] types = interfacesTypes.Concat(new Type[] { instanceType }).ToArray();
        builder.AddSingleton(instance, types);
        return builder;
    }

    public static ContainerBuilder AddSingletonSelfAndInterfaces(this ContainerBuilder builder, Type concrete)
    {
        Type[] interfacesTypes = concrete.GetInterfaces();
        Type[] types = interfacesTypes.Concat(new Type[] { concrete }).ToArray();
        builder.AddSingleton(concrete, types);
        return builder;
    }

    public static ContainerBuilder AddSingletonInterfaces(this ContainerBuilder builder, object instance)
    {
        Type instanceType = instance.GetType();
        Type[] interfacesTypes = instanceType.GetInterfaces();
        builder.AddSingleton(instance, interfacesTypes);
        return builder;
    }

    public static ContainerBuilder AddSingletonInterfaces(this ContainerBuilder builder, Type concrete)
    {
        Type[] interfacesTypes = concrete.GetInterfaces();
        builder.AddSingleton(concrete, interfacesTypes);
        return builder;
    }
}