using System;

namespace WeatherDogs
{
    [Serializable]
    public class WeatherForecastResponse
    {
        public Properties properties;

        [Serializable]
        public class Properties
        {
            public Period[] periods;
        }

        [Serializable]
        public class Period
        {
            public string temperature;
            public string temperatureUnit;
            public string icon;
        }
    }
}


