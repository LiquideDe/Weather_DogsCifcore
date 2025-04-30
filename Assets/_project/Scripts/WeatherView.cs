using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WeatherDogs
{
    public class WeatherView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textWeather;
        [SerializeField] private Image _imageWeather;

        public void SetWeather(string temperature, string unit) => _textWeather.text = $"Сегодня {temperature}{unit}";

        public void SetIcon(Sprite sprite) => _imageWeather.sprite = sprite;

        public void DestroyView() => Destroy(gameObject);

    }
}

