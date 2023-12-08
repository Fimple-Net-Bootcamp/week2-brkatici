using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace SpaceWeatherForecast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpaceWeatherController : ControllerBase
    {
        //private List<WeatherData> weatherDataList = new List<WeatherData>();
        private readonly List<WeatherData> _weatherDataList;

        public SpaceWeatherController(List<WeatherData> weatherDataList)
        {
            _weatherDataList = weatherDataList;
        }

        [HttpGet("mars/allWeatherData")]
        public IActionResult GetMarsWeather()
        {
            var marsWeather = _weatherDataList; // Rastgele veri üretimi
            return Ok(marsWeather); // JSON veri döndürme
        }

        [HttpPost("mars/weather/addWeatherData")]
        public IActionResult AddWeatherData([FromBody] WeatherData newWeatherData)
        {
            if (newWeatherData == null)
            {
                return BadRequest("Gecersiz veri. Hava durumu verisi eklenemedi.");
            }

            _weatherDataList.Add(newWeatherData);
            return Created("Yeni hava durumu verisi basariyla eklendi.", newWeatherData);
        }


        [HttpPost("mars/weather/getWeatherDataById/{dataId}")]
        public IActionResult GetWeatherDataById(int dataId)
        {
            var weatherData = _weatherDataList.FirstOrDefault(data => data.DataId == dataId);

            if (weatherData == null)
            {
                return NotFound("Belirtilen DataId ile eşleşen veri bulunamadı.");
            }

            return Ok(weatherData);
        }


        [HttpPut("mars/weather/updateWeatherData")]
        public IActionResult UpdateWeatherData([FromBody] WeatherData updatedWeatherData)
        {

            var existingData = _weatherDataList.FirstOrDefault(data => data.DataId == updatedWeatherData.DataId);

            if (existingData == null)
            {
                return NotFound("Belirtilen DataId ile eşleşen veri bulunamadı.");
            }

            // Var olan veriyi güncelliyoruz
            existingData.Temperature = updatedWeatherData.Temperature;
            existingData.Pressure = updatedWeatherData.Pressure;
            existingData.SolDay = updatedWeatherData.SolDay;
            existingData.LowestTemperature = updatedWeatherData.LowestTemperature;
            existingData.HighestTemperature = updatedWeatherData.HighestTemperature;
            existingData.LowestPressure = updatedWeatherData.LowestPressure;
            existingData.HighestPressure = updatedWeatherData.HighestPressure;
            // Diğer özellikler burada güncellenebilir

            return Ok(existingData);
        }


        [HttpPatch("mars/weather/partiallyUpdateWeatherData/{dataId}")]
        public IActionResult PartiallyUpdateWeatherData(int dataId, [FromBody] JsonPatchDocument<WeatherData> patchDoc)
        {
            var existingData = _weatherDataList.FirstOrDefault(data => data.DataId == dataId);

            if (existingData == null)
            {
                return NotFound("Belirtilen DataId ile eslesen veri bulunamadı.");
            }

            // Veriyi kısmen güncelliyoruz
            patchDoc.ApplyTo(existingData,ModelState);

            return Ok(existingData);
        }


        [HttpDelete("mars/weather/deleteWeatherData/{dataId}")]
        public IActionResult DeleteWeatherData(int dataId)
        {
            var existingData = _weatherDataList.FirstOrDefault(data => data.DataId == dataId);

            if (existingData == null)
            {
                return NotFound("Belirtilen DataId ile eşleşen veri bulunamadı.");
            }

            _weatherDataList.Remove(existingData);

            return Ok("Veri başarıyla silindi.");
        }      
    }
}
