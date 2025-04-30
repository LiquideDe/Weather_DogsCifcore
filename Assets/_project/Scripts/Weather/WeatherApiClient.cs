using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace WeatherDogs
{
    public class WeatherApiClient
    {
        private const string Endpoint = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";

        public async UniTask<string> GetForecastAsync(CancellationToken ct)
        {
            using var request = UnityWebRequest.Get(Endpoint);
            request.SetRequestHeader("User-Agent", "UnityWeatherClient");

            await request.SendWebRequest().WithCancellation(ct);

            if (request.result != UnityWebRequest.Result.Success)
                throw new Exception($"Ошибка запроса: {request.error}");

            return request.downloadHandler.text;
        }

        public async UniTask<Sprite> DownloadImageAsSprite(string url, CancellationToken ct)
        {
            using var request = UnityWebRequestTexture.GetTexture(url);
            await request.SendWebRequest().WithCancellation(ct);

            if (request.result != UnityWebRequest.Result.Success)
                throw new Exception($"Failed to download image: {request.error}");

            var texture = DownloadHandlerTexture.GetContent(request);
            var rect = new Rect(0, 0, texture.width, texture.height);
            var pivot = new Vector2(0.5f, 0.5f);
            return Sprite.Create(texture, rect, pivot);
        }
    }
}

