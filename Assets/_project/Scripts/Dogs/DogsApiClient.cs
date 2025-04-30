using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

namespace WeatherDogs
{
    public class DogsApiClient
    {
        private const string _mainUrl = "https://dogapi.dog/api/v2";

        public async UniTask<DogBreedsResponse> GetBreedsAsync(CancellationToken ct)
        {
            var url = $"{_mainUrl}/breeds";
            using var request = UnityWebRequest.Get(url);
            request.SetRequestHeader("User-Agent", "UnityDogClient");

            await request.SendWebRequest().WithCancellation(ct);

            if (request.result != UnityWebRequest.Result.Success)
                throw new Exception("Dog API error: " + request.error);

            return JsonUtility.FromJson<DogBreedsResponse>(request.downloadHandler.text);
        }

        public async UniTask<DogBreedsResponse.BreedData> GetBreedByIdAsync(string id, CancellationToken ct)
        {
            var url = $"{_mainUrl}/breeds/{id}";

            using var request = UnityWebRequest.Get(url);
            request.SetRequestHeader("User-Agent", "UnityDogClient");

            await request.SendWebRequest().WithCancellation(ct);

            if (request.result != UnityWebRequest.Result.Success)
                throw new Exception("Dog API error: " + request.error);

            var json = request.downloadHandler.text;

            var wrapper = JsonUtility.FromJson<BreedWrapper>(json);
            return wrapper.data;
        }

        public async UniTask<DogFactsResponse> GetDogFactsAsync(CancellationToken ct)
        {
            var url = $"{_mainUrl}/facts";
            using var request = UnityWebRequest.Get(url);
            request.SetRequestHeader("User-Agent", "UnityDogClient");

            await request.SendWebRequest().WithCancellation(ct);

            if (request.result != UnityWebRequest.Result.Success)
                throw new Exception("Dog API error: " + request.error);

            return JsonUtility.FromJson<DogFactsResponse>(request.downloadHandler.text);
        }
    }
}

