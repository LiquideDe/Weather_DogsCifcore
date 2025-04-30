using UnityEngine;
using Zenject;

namespace WeatherDogs
{
    public class ViewFactory
    {
        private PrefabHolder _prefabHolder;
        private DiContainer _diContaner;

        public ViewFactory(PrefabHolder prefabHolder, DiContainer diContaner)
        {
            _prefabHolder = prefabHolder;
            _diContaner = diContaner;
        }

        public GameObject Get(TypePrefab typePrefab)
        {
            GameObject instance = _diContaner.InstantiatePrefab(_prefabHolder.Get(typePrefab));
            return instance;
        }
    }
}

