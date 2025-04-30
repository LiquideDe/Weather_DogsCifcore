namespace WeatherDogs
{
    public class WindowMediator
    {
        private ViewFactory _viewFactory;
        private ServerRequestQueue _requestQueue;
        private WeatherPresenter _weatherPresenter;
        private DogsPresenter _dogsPresenter;

        public WindowMediator(ViewFactory viewFactory, ServerRequestQueue requestQueue)
        {
            _viewFactory = viewFactory;
            _requestQueue = requestQueue;
        }

        public void Start()
        {
            MainButtonsView view = _viewFactory.Get(TypePrefab.MainButtons).GetComponent<MainButtonsView>();
            view.ShowWeather += ShowWeather;
            view.ShowDogs += ShowDogs;

            WeatherView viewWeather = _viewFactory.Get(TypePrefab.Weather).GetComponent<WeatherView>();
            viewWeather.Hide();
            _weatherPresenter = new WeatherPresenter(viewWeather, _requestQueue);

            DogsView viewDogs = _viewFactory.Get(TypePrefab.Dogs).GetComponent<DogsView>();
            viewDogs.Hide();
            _dogsPresenter = new DogsPresenter(viewDogs, _requestQueue, _viewFactory);
        }

        private void ShowWeather()
        {
            _dogsPresenter.Stop();
            _weatherPresenter.Start();            
        }

        private void ShowDogs()
        {
            _weatherPresenter.Stop();
            _dogsPresenter.Start();
        }
    }
}

