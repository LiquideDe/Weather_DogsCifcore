
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Search;
using UnityEngine;

namespace WeatherDogs
{
    public class DogsPresenter
    {
        private DogsView _view;
        private ServerRequestQueue _queue;
        private DogsApiClient _client;
        private ViewFactory _viewFactory;
        private LoadingPanel _loadingPanel;
        private DogPopUp _dogPopUp;
        private CancellationTokenSource _activeBreed;
        private CancellationTokenSource _activeLoading;

        public DogsPresenter(DogsView view, ServerRequestQueue queue, ViewFactory viewFactory)
        {
            _view = view;
            _queue = queue;
            _viewFactory = viewFactory;
            _client = new DogsApiClient();
            _view.ShowThisBreed += ShowThisBreed;
        }

        public void Start()
        {
            _queue.Start();
            _view.ClearList();
            _view.Show();

            AddToQueuLoading();

            _queue.Enqueue(async token =>
            {
                var breeds = await _client.GetBreedsAsync(token);
                _view.Initialize(breeds);
                CheckAndDestroyLoadingPanel();
            });
        }

        public void Stop()
        {
            _activeBreed?.Cancel();
            _activeLoading?.Cancel();
            _queue.Stop();
            CheckAndDestroyLoadingPanel();
            _view.Hide();
            ClosePopUp();
        }

        private void ShowThisBreed(string id)
        {
            _activeBreed?.Cancel();
            CheckAndDestroyLoadingPanel();
            ClosePopUp();
            AddToQueuLoading();
            _activeBreed = new CancellationTokenSource();
            var token = _activeBreed.Token;
            _queue.Enqueue(async queueToken =>
            {
                using var linked = CancellationTokenSource.CreateLinkedTokenSource(queueToken, token);
                try
                {
                    var breed = await _client.GetBreedByIdAsync(id, linked.Token);
                    CreatePopUp(breed.attributes.name, breed.attributes.description);
                    CheckAndDestroyLoadingPanel();
                }
                catch (OperationCanceledException)
                {
                    Debug.Log("Запрос породы отменён (выбрана другая)");
                }
                
            });
        }

        private void CheckAndDestroyLoadingPanel()
        {
            if (_loadingPanel != null)
                _loadingPanel.DestroyPanel();
        }

        private void CheckAndCreateLoadingPanel()
        {
            if (_loadingPanel == null)
                _loadingPanel = _viewFactory.Get(TypePrefab.Loading).GetComponent<LoadingPanel>();
        }

        private void CreatePopUp(string name, string description)
        {
            _dogPopUp = _viewFactory.Get(TypePrefab.DogPopUp).GetComponent<DogPopUp>();
            _dogPopUp.Initialize(name, description);
        }

        private void ClosePopUp()
        {
            if (_dogPopUp != null)
                _dogPopUp.Hide();
        }

        private void AddToQueuLoading()
        {
            _activeLoading?.Cancel();
            _activeLoading = new CancellationTokenSource();
            var token = _activeLoading.Token;
            _queue.Enqueue(async queueToken =>
            {
            using var linked = CancellationTokenSource.CreateLinkedTokenSource(queueToken, token);
                try
                {
                    CheckAndCreateLoadingPanel();
                    var facts = await _client.GetDogFactsAsync(linked.Token);
                    if (facts.data.Length > 0)
                        _loadingPanel.Initialize(facts.data[0].attributes.body);
                    //await UniTask.Delay(1000);
                }
                catch (OperationCanceledException)
                {
                    Debug.Log("Запрос отменён ");
                }

            });
        }
    }
}


