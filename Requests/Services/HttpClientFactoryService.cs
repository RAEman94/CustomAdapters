using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using Requests.Clients;
using System.Text;
using System.Text.Json;

namespace Requests.Services
{
    public class HttpClientFactoryService<T> : IHttpClientServiceImplementation
    {
        [Required(ErrorMessage ="Uri is reqired!")]
        public string Uri { get; set; }
        [Required(ErrorMessage = "ClientName is reqired!")]
        public string ClientName { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Client<T> _client;
        private readonly JsonSerializerOptions _options;

        public HttpClientFactoryService(IHttpClientFactory httpClientFactory,Client<T> client)
        {
            _httpClientFactory = httpClientFactory;
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task Execute()
        {
            await GetWithTypedClient();
        }
        private async Task GetWithHttpClientFactory()
        {
            var httpClient = _httpClientFactory.CreateClient(ClientName);

            using (var response = await httpClient.GetAsync(Uri, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();

                var companies = await JsonSerializer.DeserializeAsync<T>(stream, _options);
            }
        }
        private async Task GetWithTypedClient() => await _client.Get(Uri);
    }
}
