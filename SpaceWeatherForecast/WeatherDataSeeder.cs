namespace SpaceWeatherForecast
{
    public class WeatherDataSeeder
    {
        public List<WeatherData> SeedWeatherData()
        {
            var weatherDataList = new List<WeatherData>();


            for (int i = 0; i < 20; i++)
            {
                var random = new Random();
                var weatherData = new WeatherData
                {
                    DataId = i,
                    Temperature = random.Next(-100, 100),
                    Pressure = random.Next(700, 1200),
                    SolDay = random.Next(1, 1000),
                    LowestTemperature = random.Next(-120, -50),
                    HighestTemperature = random.Next(20, 50),
                    LowestPressure = random.Next(600, 700),
                    HighestPressure = random.Next(1200, 1400),
                    Status = true
                    // Diğer özellikler burada eklenebilir
                };

                weatherDataList.Add(weatherData);
            }

            return weatherDataList;
        }
    }
}
