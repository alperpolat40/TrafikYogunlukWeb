using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TrafikYogunlukWeb.Services  // <--- Burası ÖNEMLİ
{
    public class TomTomService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "aTLWKOqcNMilqaJA2ff9un1gJxLO9azf"; // Değiştirilebilir

        public TomTomService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<JObject>> GetMultipleTrafficFlowsAsync(List<(double lat, double lon)> points)
        {
            var tasks = points.Select(async point =>
            {
                var url = $"https://api.tomtom.com/traffic/services/4/flowSegmentData/absolute/10/json" +
                          $"?point={point.lat.ToString(CultureInfo.InvariantCulture)}," +
                          $"{point.lon.ToString(CultureInfo.InvariantCulture)}" +
                          $"&unit=KMPH&key={_apiKey}";

                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API Hatası: {response.StatusCode} - {errorContent}");
                }

                var json = await response.Content.ReadAsStringAsync();
                return JObject.Parse(json);
            });

            return (await Task.WhenAll(tasks)).ToList();
        }
    }
}
