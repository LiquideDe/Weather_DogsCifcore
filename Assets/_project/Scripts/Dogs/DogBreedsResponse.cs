using System;

namespace WeatherDogs
{
    [Serializable]
    public class DogBreedsResponse
    {
        public BreedData[] data;

        [Serializable]
        public class BreedData
        {
            public string id;
            public BreedAttributes attributes;
        }

        [Serializable]
        public class BreedAttributes
        {
            public string name;
            public string description;
        }
    }
}


