using UnityEngine;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace WeatherDogs
{
    public class WeatherPresenter
    {
        private WeatherView _view;
        private WeatherApiClient _client;
        private ServerRequestQueue _queue;
        private CancellationTokenSource _loopCts;

        public WeatherPresenter(WeatherView view, ServerRequestQueue queue)
        {
            _view = view;
            _client = new WeatherApiClient();
            _queue = queue;   
        }

        public void Start()
        {
            _view.Show();
            _queue.Start();
            _loopCts = new CancellationTokenSource();
            StartAddingRequests(_loopCts.Token).Forget();
        }

        public void Stop()
        {
            _view.Hide();
            _loopCts?.Cancel();
            _queue?.Stop();
        }

        private async UniTaskVoid StartAddingRequests(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                _queue.Enqueue(async token =>
                {
                    var result = await _client.GetForecastAsync(token);
                    var data = JsonUtility.FromJson<WeatherForecastResponse>(result);
                    _view.SetWeather(data.properties.periods[0].temperature, data.properties.periods[0].temperatureUnit);
                    var sprite = await _client.DownloadImageAsSprite(data.properties.periods[0].icon, token);
                    _view.SetIcon(sprite);
                });

                try
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(5), cancellationToken: ct);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }  
    }
}

