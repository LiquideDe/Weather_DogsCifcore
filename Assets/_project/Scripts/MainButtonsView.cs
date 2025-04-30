using UnityEngine;
using UnityEngine.UI;
using System;

namespace WeatherDogs
{
    public class MainButtonsView : MonoBehaviour
    {
        public event Action ShowWeather;
        public event Action ShowDogs;

        [SerializeField] private Button _buttonWeather, _buttonDogs;

        private void OnEnable()
        {
            _buttonWeather.onClick.AddListener(ShowWeatherPressed);
            _buttonDogs.onClick.AddListener(ShowDogsPressed);
        }

        private void OnDisable()
        {
            _buttonWeather.onClick?.RemoveAllListeners();
            _buttonDogs.onClick?.RemoveAllListeners();
        }

        private void ShowWeatherPressed()
        {
            _buttonWeather.image.color = Color.green;
            _buttonDogs.image.color = Color.white;
            ShowWeather?.Invoke();
        }

        private void ShowDogsPressed() 
        {
            _buttonWeather.image.color = Color.white;
            _buttonDogs.image.color = Color.green;
            ShowDogs?.Invoke();
        }
    }
}

