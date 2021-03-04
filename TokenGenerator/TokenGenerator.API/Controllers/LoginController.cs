using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TokenGenerator.Domain.Models;
using TokenGenerator.Services;

namespace TokenGenerator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> LoginAsync([FromBody] UserModel model )
        {
            var user = await RequestUser(model);

            if (user?.Email == null)
            {
                return NotFound(model.Email);
            }

            if (!user.Password.Equals(model.Password))
            {
                return Unauthorized("Bad password");
            }

            string token = JwtManager.GenerateToken(model.Email);

            return Ok(token);
        }

        private static async Task<User> RequestUser(UserModel model)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri($"https://localhost:5001/api/Users/email/{model.Email}");
            var request = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = HttpMethod.Get
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.SendAsync(request);
            var jsonString = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(jsonString);

            return user;
        }

        //private string GenerateJSONWebToken(UserModel userInfo)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //      _config["Jwt:Issuer"],
        //      null,
        //      expires: DateTime.Now.AddMinutes(120),
        //      signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}
