namespace WeatherDogs
{
    public class WindowMediator
    {
        private ViewFactory _viewFactory;
        private ServerRequestQueue _requestQueue;
        WeatherPresenter _weatherPresenter;

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
        }

        private void ShowWeather()
        {
            WeatherView view = _viewFactory.Get(TypePrefab.Weather).GetComponent<WeatherView>();
            _weatherPresenter = new WeatherPresenter(view, _requestQueue);
        }

        private void ShowDogs()
        {
            if(_weatherPresenter != null)
                _weatherPresenter.Stop();
        }
    }
}

