using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace WeatherDogs
{
    public class FactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ViewFactory>().AsSingle();
        }
    }
}

