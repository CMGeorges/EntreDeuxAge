using System.Net.Http;
using BlazorEntre2Ages.Models;
using Microsoft.Extensions.Options;

namespace BlazorEntre2Ages.Services
{
    public class EventService : IEventService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _settings;

        public EventService(HttpClient httpClient, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _settings = optionsMonitor.CurrentValue;

            httpClient.DefaultRequestHeaders.Add("User-Agent", "Entre2Ages");

            _httpClient = httpClient;
        }
    }
}