using UnityEngine;

namespace WeatherDogs
{
    [CreateAssetMenu(fileName = "PrefabHolder", menuName = "Holder/PrefabHolder")]
    public class PrefabHolder : ScriptableObject
    {
        [SerializeField] private GameObject MainButtonsPrefab, WeatherPrefab;

        public GameObject Get(TypePrefab typePrefab)
        {
            switch (typePrefab)
            {
                case TypePrefab.MainButtons:
                    return MainButtonsPrefab;

                case TypePrefab.Weather:
                    return WeatherPrefab;

                default:
                    throw new System.Exception($"No prefab");
            }
        }
    }
}

