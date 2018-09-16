using System.Collections.Generic;

namespace Kata04.Weather
{
    public class WeatherFileData
    {
        private readonly List<WeatherData> _weatherData;

        public WeatherFileData()
        {
            _weatherData = new List<WeatherData>();
        }
        public IReadOnlyList<WeatherData> WeatherData => _weatherData;
        public string HeaderText { get; set; }
        public string SummaryText { get; set; }

        public void AddWeatherData(WeatherData weatherData)
        {
            _weatherData.Add(weatherData);
        }
    }
}