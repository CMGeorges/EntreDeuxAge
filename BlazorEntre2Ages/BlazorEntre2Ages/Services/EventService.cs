using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorEntre2Ages.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

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

        public async Task<List<Event>> GetAll()
        {
            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_settings.Url +"api/Events/"),
            };

            var response = await _httpClient.SendAsync(requestMessage);
            var responseBody = await response.Content.ReadAsStringAsync();

            var events = JsonConvert.DeserializeObject<List<Event>>(responseBody);
            return events;
        }

        public Task<Event> Create(Event @event)
        {
            throw new System.NotImplementedException();
        }

        public Task<Event> Update(Event @event)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(Event @event)
        {
            throw new System.NotImplementedException();
        }
    }
}