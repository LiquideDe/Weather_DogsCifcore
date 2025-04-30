using System;

namespace WeatherDogs
{
    [Serializable]
    public class DogFactsResponse
    {
        public FactData[] data;

        [Serializable]
        public class FactData
        {
            public string id;
            public string type;
            public FactAttributes attributes;
        }

        [Serializable]
        public class FactAttributes
        {
            public string body;
        }
    }
}


