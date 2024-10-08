﻿using Balls.Source.Infrastructure.Factories;
using Balls.Source.View.Factories;
using Reflex.Core;
using UnityEngine;

namespace Balls.Source.Infrastructure.Installers
{
    public sealed class FactoriesInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingletonInterfaces(typeof(GameBoardModulesFactory))
                .AddSingletonInterfaces(typeof(JobFactory));
        }
    }
}