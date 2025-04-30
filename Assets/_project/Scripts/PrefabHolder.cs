using UnityEngine;

namespace WeatherDogs
{
    [CreateAssetMenu(fileName = "PrefabHolder", menuName = "Holder/PrefabHolder")]
    public class PrefabHolder : ScriptableObject
    {
        [SerializeField] private GameObject MainButtonsPrefab, WeatherViewPrefab, DogViewPrefab, LoadingDogPrefab, DogPopUpPrefab;

        public GameObject Get(TypePrefab typePrefab)
        {
            switch (typePrefab)
            {
                case TypePrefab.MainButtons:
                    return MainButtonsPrefab;

                case TypePrefab.Weather:
                    return WeatherViewPrefab;

                case TypePrefab.Dogs:
                    return DogViewPrefab;

                case TypePrefab.Loading:
                    return LoadingDogPrefab;

                case TypePrefab.DogPopUp:
                    return DogPopUpPrefab;

                default:
                    throw new System.Exception($"No prefab");
            }
        }
    }
}

