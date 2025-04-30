
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


            CheckAndCreateLoadingPanel();
            _queue.Enqueue(async token =>
            {
                var facts = await _client.GetDogFactsAsync(token);
                _loadingPanel.Initialize(facts.data[0].attributes.body);
            });

            _queue.Enqueue(async token =>
            {
                var breeds = await _client.GetBreedsAsync(token);
                _view.Initialize(breeds);
                CheckAndDestroyLoadingPanel();
            });
        }

        public void Stop()
        {
            _queue.Stop();
            CheckAndDestroyLoadingPanel();
            _view.Hide();
            ClosePopUp();
        }

        private void ShowThisBreed(string id)
        {
            _queue.Stop();
            _queue.Start();
            CheckAndDestroyLoadingPanel();
            CheckAndCreateLoadingPanel();
            ClosePopUp();
            _queue.Enqueue(async token =>
            {
                var facts = await _client.GetDogFactsAsync(token);
                _loadingPanel.Initialize(facts.data[0].attributes.body);
            });
            _queue.Enqueue(async token =>
            {
                var breed = await _client.GetBreedByIdAsync(id, token);
                CreatePopUp(breed.attributes.name, breed.attributes.description);
                CheckAndDestroyLoadingPanel();
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
            if(_dogPopUp != null)
                _dogPopUp.Hide();
        }
    }
}


