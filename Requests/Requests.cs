using Microsoft.Extensions.DependencyInjection;
using Requests.Clients;
using Requests.Services;
using System;
using System.Threading.Tasks;

namespace Requests
{
    public class Requests<T>
	{
		private string _uri;
		private string _clientName;
		public async Task Get(string uri,string clientName = "defaultClient")
        {
			_clientName = clientName;
			var services = new ServiceCollection();
			_uri = uri;
			ConfigureServices(services);
			var provider = services.BuildServiceProvider();
			try
			{
				await provider.GetRequiredService<IHttpClientServiceImplementation>()
					.Execute();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Something went wrong: {ex}");
			}
		}
		private void ConfigureServices(IServiceCollection services)
		{
			services.AddHttpClient(_clientName, config =>
			{
				config.BaseAddress = new Uri(_uri);
				config.Timeout = new TimeSpan(0, 0, 30);
				config.DefaultRequestHeaders.Clear();
			});

			services.AddHttpClient<Client<T>>();

			//services.AddScoped<IHttpClientServiceImplementation, HttpClientCrudService>();
			//services.AddScoped<IHttpClientServiceImplementation, HttpClientPatchService>();
			//services.AddScoped<IHttpClientServiceImplementation, HttpClientStreamService>();
			//services.AddScoped<IHttpClientServiceImplementation, HttpClientCancellationService>();
			services.AddScoped<IHttpClientServiceImplementation, HttpClientFactoryService<T>>();
		}
	}
}
