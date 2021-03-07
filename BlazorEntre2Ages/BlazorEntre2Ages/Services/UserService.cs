using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlazorEntre2Ages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BlazorEntre2Ages.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _settings;

        public UserService(HttpClient httpClient, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _settings = optionsMonitor.CurrentValue;

            httpClient.DefaultRequestHeaders.Add("User-Agent", "Entre2Ages");

            _httpClient = httpClient;
        }

        public async Task<User> LoginAsync(User user)
        {
            user.Password = Encrypt(user.Password);
            var serializedUser = JsonConvert.SerializeObject(user);

            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_settings.Url+"api/Users/Login"),
                Content = new StringContent(serializedUser, Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(requestMessage);
            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();
            var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);
            
            return await Task.FromResult(returnedUser);
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            user.Password = Encrypt(user.Password);
            var serializedUser = JsonConvert.SerializeObject(user);

            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_settings.Url +"api/Users/Register"),
                Content = new StringContent(serializedUser, Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

            return await Task.FromResult(returnedUser);
        }

        public async Task<User> RefreshTokenAsync(RefreshRequest refreshRequest)
        {
            var serializedUser = JsonConvert.SerializeObject(refreshRequest);

            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_settings.Url+"api/Users/RefreshToken"),
                Content = new StringContent(serializedUser, Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

            return await Task.FromResult(returnedUser);
        }

        public async Task<User> GetUserByAccessTokenAsync(string accessToken)
        {
            var serializedRefreshRequest = JsonConvert.SerializeObject(accessToken);

            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_settings.Url+"api/Users/GetUserByAccessToken"),
                Content = new StringContent(serializedRefreshRequest, Encoding.UTF8, "application/json")
            };
            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();
            
            var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

            return await Task.FromResult(returnedUser);
        }
        
        public static string Encrypt(string password)
        {
            var provider = MD5.Create();
            const string salt = "RandomSalt";            
            var bytes = provider.ComputeHash(Encoding.UTF32.GetBytes(salt + password));
            return BitConverter.ToString(bytes).Replace("-","").ToLower();
        }
    }
}