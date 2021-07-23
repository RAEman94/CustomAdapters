using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Requests.Clients
{
    public class Client<T>
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        private TimeSpan defaultTimeSpan = new TimeSpan(0, 0, 30);
        public Client(HttpClient client,string uri,TimeSpan? timeSpan)
        {
            _client = client;
            _client.BaseAddress = new Uri(uri);
            _client.Timeout = (timeSpan == null) ? defaultTimeSpan : (TimeSpan)timeSpan;
            _client.DefaultRequestHeaders.Clear();

            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<T> Get(string uri)
        {
            using(var response = await _client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<T>(stream, _options);
                return result;
            }
        }
    }
}
